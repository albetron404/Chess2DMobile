#if UNITY_EDITOR
using UnityEngine;
using System;
using Chess2DGen;

public static class QueenDrawer
{
    public static Texture2D Draw(Color fill, Color stroke)
    {
        var t = PieceDrawUtil.MakeBlank(); int cx = PieceDrawUtil.TEX / 2;
        Action<Color> fillBody = (c) => {
            PieceDrawUtil.FillEllipse(t, cx, 300, 160, 140, c);
            PieceDrawUtil.FillRect(t, cx - 180, 220, 360, 40, c);
            DrawCrown(t, cx, 430, 170, 60, 0, c);
        };
        Action<Color> strokeBody = (c) => {
            PieceDrawUtil.FillEllipse(t, cx, 300, 160 + PieceDrawUtil.STROKE, 140 + PieceDrawUtil.STROKE, c);
            PieceDrawUtil.FillRect(t, cx - (180 + PieceDrawUtil.STROKE), 220 - PieceDrawUtil.STROKE, 360 + 2 * PieceDrawUtil.STROKE, 40 + PieceDrawUtil.STROKE, c);
            DrawCrown(t, cx, 430, 170, 60, PieceDrawUtil.STROKE, c);
        };
        PieceDrawUtil.WithStroke(t, fillBody, strokeBody, fill, stroke);
        t.Apply(); return t;
    }

    static void DrawCrown(Texture2D t, int cx, int yBase, int halfWidth, int height, int expand, Color col)
    {
        int step = halfWidth * 2 / 6;
        for (int i = 0; i < 5; i++)
        {
            int x = cx - halfWidth + step * (i + 1);
            var a = new Vector2Int(x, yBase + height + expand);
            var b = new Vector2Int(x - step + expand, yBase - expand);
            var c = new Vector2Int(x + step - expand, yBase - expand);
            PieceDrawUtil.FillTri(t, a, b, c, col);
        }
    }
}
#endif
