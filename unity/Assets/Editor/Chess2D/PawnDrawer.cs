#if UNITY_EDITOR
using UnityEngine;
using Chess2DGen;

public static class PawnDrawer
{
    public static Texture2D Draw(Color fill, Color stroke)
    {
        var t = PieceDrawUtil.MakeBlank();
        int cx = PieceDrawUtil.TEX / 2;
        int th = PieceDrawUtil.STROKE;

        // --- Head (circle)
        int headCy = Mathf.RoundToInt(0.70f * PieceDrawUtil.TEX);
        int headR = Mathf.RoundToInt(0.12f * PieceDrawUtil.TEX);

        // --- Neck (vertical rect)
        int neckW = Mathf.RoundToInt(0.10f * PieceDrawUtil.TEX);
        int neckH = Mathf.RoundToInt(0.60f * PieceDrawUtil.TEX);
        int neckX = cx - neckW / 2;
        int neckY = Mathf.RoundToInt(0.55f * PieceDrawUtil.TEX);

        // --- Base1 (wide thin rectangle)
        int base1W = Mathf.RoundToInt(0.55f * PieceDrawUtil.TEX);
        int base1H = Mathf.RoundToInt(0.08f * PieceDrawUtil.TEX);
        int base1X = cx - base1W / 2;
        int base1Y = Mathf.RoundToInt(0.35f * PieceDrawUtil.TEX);

        // --- Base2 (slightly wider rectangle below)
        int base2W = Mathf.RoundToInt(0.70f * PieceDrawUtil.TEX);
        int base2H = Mathf.RoundToInt(0.10f * PieceDrawUtil.TEX);
        int base2X = cx - base2W / 2;
        int base2Y = Mathf.RoundToInt(0.25f * PieceDrawUtil.TEX);

        // FILL = carve inside, STROKE = outer border
        System.Action<Color> drawFill = (c) =>
        {
            PieceDrawUtil.FillCircle(t, cx, headCy, headR, Color.clear);
            PieceDrawUtil.FillRect(t, neckX, neckY, neckW, neckH, Color.clear);
            PieceDrawUtil.FillRect(t, base1X, base1Y, base1W, base1H, Color.clear);
            PieceDrawUtil.FillRect(t, base2X, base2Y, base2W, base2H, Color.clear);
        };

        System.Action<Color> drawStroke = (c) =>
        {
            PieceDrawUtil.FillCircle(t, cx, headCy, headR + th, c);
            PieceDrawUtil.FillRect(t, neckX - th / 2, neckY - th / 2, neckW + th, neckH + th, c);
            PieceDrawUtil.FillRect(t, base1X - th / 2, base1Y - th / 2, base1W + th, base1H + th, c);
            PieceDrawUtil.FillRect(t, base2X - th / 2, base2Y - th / 2, base2W + th, base2H + th, c);
        };

        PieceDrawUtil.WithStroke(t, drawFill, drawStroke, Color.clear, stroke);
        return t;
    }
}
#endif
