#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

namespace Chess2DGen
{
    public static class PieceDrawUtil
    {
        public const int TEX = 512;
        public const int PPU = 256;
        public const int STROKE = 12;

        public static Texture2D MakeBlank()
        {
            var t = new Texture2D(TEX, TEX, TextureFormat.RGBA32, false);
            var px = new Color[TEX * TEX];
            for (int i = 0; i < px.Length; i++) px[i] = new Color(0, 0, 0, 0);
            t.SetPixels(px);
            t.wrapMode = TextureWrapMode.Clamp;
            return t;
        }
        public static void Apply(this Texture2D t) => t.Apply();

        public static void FillRect(Texture2D t, int x, int y, int w, int h, Color c)
        {
            int x0 = Mathf.Clamp(x, 0, TEX - 1), y0 = Mathf.Clamp(y, 0, TEX - 1);
            int x1 = Mathf.Clamp(x + w, 0, TEX), y1 = Mathf.Clamp(y + h, 0, TEX);
            for (int yy = y0; yy < y1; yy++)
                for (int xx = x0; xx < x1; xx++)
                    t.SetPixel(xx, yy, c);
        }
        public static void FillCircle(Texture2D t, int cx, int cy, int r, Color c)
        {
            int r2 = r * r;
            for (int y = cy - r; y <= cy + r; y++)
            {
                if (y < 0 || y >= TEX) continue;
                int dy = y - cy; int dy2 = dy * dy;
                for (int x = cx - r; x <= cx + r; x++)
                {
                    if (x < 0 || x >= TEX) continue;
                    int dx = x - cx; if (dx * dx + dy2 <= r2) t.SetPixel(x, y, c);
                }
            }
        }
        public static void FillEllipse(Texture2D t, int cx, int cy, int rx, int ry, Color c)
        {
            float rx2 = rx * rx, ry2 = ry * ry;
            for (int y = cy - ry; y <= cy + ry; y++)
            {
                if (y < 0 || y >= TEX) continue;
                float dy = y - cy; float v = (dy * dy) / ry2;
                for (int x = cx - rx; x <= cx + rx; x++)
                {
                    if (x < 0 || x >= TEX) continue;
                    float dx = x - cx; if ((dx * dx) / rx2 + v <= 1f) t.SetPixel(x, y, c);
                }
            }
        }
        public static void FillTri(Texture2D t, Vector2Int a, Vector2Int b, Vector2Int c, Color col)
        {
            int minX = Mathf.Max(0, Mathf.Min(a.x, Mathf.Min(b.x, c.x)));
            int maxX = Mathf.Min(TEX - 1, Mathf.Max(a.x, Mathf.Max(b.x, c.x)));
            int minY = Mathf.Max(0, Mathf.Min(a.y, Mathf.Min(b.y, c.y)));
            int maxY = Mathf.Min(TEX - 1, Mathf.Max(a.y, Mathf.Max(b.y, c.y)));
            float area = (b.x - a.x) * (c.y - a.y) - (b.y - a.y) * (c.x - a.x); if (Mathf.Abs(area) < 1e-5f) return;
            for (int y = minY; y <= maxY; y++)
                for (int x = minX; x <= maxX; x++)
                {
                    float w0 = (b.x - a.x) * (y - a.y) - (b.y - a.y) * (x - a.x);
                    float w1 = (c.x - b.x) * (y - b.y) - (c.y - b.y) * (x - b.x);
                    float w2 = (a.x - c.x) * (y - c.y) - (a.y - c.y) * (x - c.x);
                    bool neg = (w0 < 0) || (w1 < 0) || (w2 < 0), pos = (w0 > 0) || (w1 > 0) || (w2 > 0);
                    if (!(neg && pos)) t.SetPixel(x, y, col);
                }
        }
        public static void DrawPolygon(Texture2D t, List<Vector2Int> pts, Color col)
        {
            for (int i = 1; i < pts.Count - 1; i++) FillTri(t, pts[0], pts[i], pts[i + 1], col);
        }
        public static List<Vector2Int> Inflate(List<Vector2Int> pts, int by)
        {
            Vector2 centroid = Vector2.zero; foreach (var p in pts) centroid += (Vector2)p; centroid /= pts.Count;
            var outPts = new List<Vector2Int>(pts.Count);
            foreach (var p in pts)
            {
                var dir = ((Vector2)p - centroid).normalized;
                outPts.Add(new Vector2Int(Mathf.RoundToInt(p.x + dir.x * by), Mathf.RoundToInt(p.y + dir.y * by)));
            }
            return outPts;
        }

        public static void SaveSprite(Texture2D tex, string path)
        {
            File.WriteAllBytes(path, tex.EncodeToPNG());
            AssetDatabase.ImportAsset(path);
            var ti = (TextureImporter)AssetImporter.GetAtPath(path);
            ti.textureType = TextureImporterType.Sprite;
            ti.spritePixelsPerUnit = PPU;
            ti.alphaIsTransparency = true; ti.mipmapEnabled = false; ti.filterMode = FilterMode.Point;
            ti.SaveAndReimport();
        }

        public static Texture2D WithStroke(Texture2D baseTex, System.Action<Color> drawFill, System.Action<Color> drawStroke, Color fill, Color stroke)
        {
            // draw stroke bigger, then fill exact
            drawStroke(stroke);
            drawFill(fill);
            baseTex.Apply();
            return baseTex;
        }
    }
}
#endif
