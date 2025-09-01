#if UNITY_EDITOR
using UnityEngine;
using System;
using Chess2DGen;

public static class BishopDrawer
{
    // Keep the same signature as your other drawers.
    // We'll ignore 'fill' (we want outline-only), and use 'stroke' for the white/black outline.
    public static Texture2D Draw(Color fill, Color stroke)
    {
        var t = PieceDrawUtil.MakeBlank();
        int cx = PieceDrawUtil.TEX / 2;

        // Proportions tuned for icon look (works with TEX=512, PPU=256, STROKE=12)
        // Positions are expressed as fractions of TEX for clarity.
        int baseCy = Mathf.RoundToInt(0.32f * PieceDrawUtil.TEX);
        int baseRx = Mathf.RoundToInt(0.36f * PieceDrawUtil.TEX);
        int baseRy = Mathf.RoundToInt(0.09f * PieceDrawUtil.TEX);

        int torsoCy = Mathf.RoundToInt(0.56f * PieceDrawUtil.TEX);
        int torsoRx = Mathf.RoundToInt(0.25f * PieceDrawUtil.TEX);
        int torsoRy = Mathf.RoundToInt(0.26f * PieceDrawUtil.TEX);

        int headCy = Mathf.RoundToInt(0.78f * PieceDrawUtil.TEX);
        int headR = Mathf.RoundToInt(0.12f * PieceDrawUtil.TEX);

        int th = PieceDrawUtil.STROKE; // outline thickness

        // We want rings (outline only). Trick:
        //   draw "stroke" shape larger, then draw the SAME shape smaller with Color.clear to carve the inside.
        // Use WithStroke to keep the pattern consistent with your other drawers.
        Action<Color> drawFill = (c) =>
        {
            // carve the inside with transparent fill
            PieceDrawUtil.FillEllipse(t, cx, baseCy, baseRx, baseRy, Color.clear);
            PieceDrawUtil.FillEllipse(t, cx, torsoCy, torsoRx, torsoRy, Color.clear);
            PieceDrawUtil.FillCircle(t, cx, headCy, headR, Color.clear);

            // draw the + inside the head (solid lines)
            int crossR = Mathf.RoundToInt(0.065f * PieceDrawUtil.TEX);
            // horizontal
            PieceDrawUtil.FillRect(t, cx - crossR, headCy - th / 2, 2 * crossR, th, stroke);
            // vertical
            PieceDrawUtil.FillRect(t, cx - th / 2, headCy - crossR, th, 2 * crossR, stroke);
        };

        Action<Color> drawStroke = (c) =>
        {
            // outer (thicker) shapes for the ring border
            PieceDrawUtil.FillEllipse(t, cx, baseCy, baseRx + th, baseRy + th, c);
            PieceDrawUtil.FillEllipse(t, cx, torsoCy, torsoRx + th, torsoRy + th, c);
            PieceDrawUtil.FillCircle(t, cx, headCy, headR + th, c);
        };

        PieceDrawUtil.WithStroke(t, drawFill, drawStroke, /*fill*/Color.clear, /*stroke*/stroke);
        return t;
    }
}
#endif
