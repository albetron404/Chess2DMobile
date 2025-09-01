#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;
using Chess2DGen;

public static class GenerateChessPieces2D
{
    [MenuItem("Tools/Chess2D/Generate Pieces + Tiles")]
    public static void GenerateAll()
    {
        string resDir = "Assets/Resources";
        string spritesDir = $"{resDir}/Sprites";
        string prefabsDir = $"{resDir}/Prefabs";
        Directory.CreateDirectory(resDir);
        Directory.CreateDirectory(spritesDir);
        Directory.CreateDirectory(prefabsDir);

        // Tiles
        SaveSolid(spritesDir, "TileLight.png", new Color(0.92f, 0.84f, 0.70f));
        SaveSolid(spritesDir, "TileDark.png", new Color(0.55f, 0.41f, 0.30f));

        // Pieces: index order 0..5 white P,N,B,R,Q,K ; 6..11 black P..K
        var whiteFill = Color.white; var whiteStroke = new Color(0.08f, 0.08f, 0.10f);
        var blackFill = new Color(0.12f, 0.12f, 0.14f); var blackStroke = Color.white;

        var tP = PawnDrawer.Draw(whiteFill, whiteStroke);
        var tN = KnightDrawer.Draw(whiteFill, whiteStroke);
        var tB = BishopDrawer.Draw(whiteFill, whiteStroke);
        var tR = RookDrawer.Draw(whiteFill, whiteStroke);
        var tQ = QueenDrawer.Draw(whiteFill, whiteStroke);
        var tK = KingDrawer.Draw(whiteFill, whiteStroke);

        var tP2 = PawnDrawer.Draw(blackFill, blackStroke);
        var tN2 = KnightDrawer.Draw(blackFill, blackStroke);
        var tB2 = BishopDrawer.Draw(blackFill, blackStroke);
        var tR2 = RookDrawer.Draw(blackFill, blackStroke);
        var tQ2 = QueenDrawer.Draw(blackFill, blackStroke);
        var tK2 = KingDrawer.Draw(blackFill, blackStroke);

        PieceDrawUtil.SaveSprite(tP, $"{spritesDir}/Piece_0.png");
        PieceDrawUtil.SaveSprite(tN, $"{spritesDir}/Piece_1.png");
        PieceDrawUtil.SaveSprite(tB, $"{spritesDir}/Piece_2.png");
        PieceDrawUtil.SaveSprite(tR, $"{spritesDir}/Piece_3.png");
        PieceDrawUtil.SaveSprite(tQ, $"{spritesDir}/Piece_4.png");
        PieceDrawUtil.SaveSprite(tK, $"{spritesDir}/Piece_5.png");

        PieceDrawUtil.SaveSprite(tP2, $"{spritesDir}/Piece_6.png");
        PieceDrawUtil.SaveSprite(tN2, $"{spritesDir}/Piece_7.png");
        PieceDrawUtil.SaveSprite(tB2, $"{spritesDir}/Piece_8.png");
        PieceDrawUtil.SaveSprite(tR2, $"{spritesDir}/Piece_9.png");
        PieceDrawUtil.SaveSprite(tQ2, $"{spritesDir}/Piece_10.png");
        PieceDrawUtil.SaveSprite(tK2, $"{spritesDir}/Piece_11.png");

        // Prefabs
        var tileGO = new GameObject("Tile");
        tileGO.AddComponent<SpriteRenderer>().sprite = AssetDatabase.LoadAssetAtPath<Sprite>($"{spritesDir}/TileLight.png");
        PrefabUtility.SaveAsPrefabAsset(tileGO, $"{prefabsDir}/Tile.prefab");
        Object.DestroyImmediate(tileGO);

        var pieceGO = new GameObject("Piece");
        pieceGO.AddComponent<SpriteRenderer>();
        PrefabUtility.SaveAsPrefabAsset(pieceGO, $"{prefabsDir}/Piece.prefab");
        Object.DestroyImmediate(pieceGO);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("[Chess2D] Pieces + tiles regenerated.");
    }

    static void SaveSolid(string dir, string name, Color c)
    {
        var t = new Texture2D(PieceDrawUtil.TEX, PieceDrawUtil.TEX, TextureFormat.RGBA32, false);
        var px = new Color[PieceDrawUtil.TEX * PieceDrawUtil.TEX]; for (int i = 0; i < px.Length; i++) px[i] = c;
        t.SetPixels(px); t.Apply();
        PieceDrawUtil.SaveSprite(t, $"{dir}/{name}");
    }
}
#endif
