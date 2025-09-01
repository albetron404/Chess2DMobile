#if UNITY_EDITOR
using UnityEngine;
using System;
using Chess2DGen;

public static class RookDrawer
{
    public static Texture2D Draw(Color fill, Color stroke)
    {
        var t = PieceDrawUtil.MakeBlank(); int cx = PieceDrawUtil.TEX / 2;
        Action<Color> fillBody = (c) => {
            PieceDrawUtil.FillRect(t, cx - 130, 220, 260, 220, c);
            PieceDrawUtil.FillRect(t, cx - 140, 430, 280, 30, c);
            PieceDrawUtil.FillRect(t, cx - 110, 460, 50, 40, c);
            PieceDrawUtil.FillRect(t, cx - 25, 460, 50, 40, c);
            PieceDrawUtil.FillRect(t, cx + 60, 460, 50, 40, c);
        };
        Action<Color> strokeBody = (c) => {
            PieceDrawUtil.FillRect(t, cx - (130 + PieceDrawUtil.STROKE), 220 - PieceDrawUtil.STROKE, 260 + 2 * PieceDrawUtil.STROKE, 220 + PieceDrawUtil.STROKE, c);
            PieceDrawUtil.FillRect(t, cx - 140, 430, 280, 30 + PieceDrawUtil.STROKE, c);
            PieceDrawUtil.FillRect(t, cx - 110 - PieceDrawUtil.STROKE, 460, 50 + 2 * PieceDrawUtil.STROKE, 40 + PieceDrawUtil.STROKE, c);
            PieceDrawUtil.FillRect(t, cx - 25 - PieceDrawUtil.STROKE, 460, 50 + 2 * PieceDrawUtil.STROKE, 40 + PieceDrawUtil.STROKE, c);
            PieceDrawUtil.FillRect(t, cx + 60 - PieceDrawUtil.STROKE, 460, 50 + 2 * PieceDrawUtil.STROKE, 40 + PieceDrawUtil.STROKE, c);
        };
        return PieceDrawUtil.WithStroke(t, fillBody, strokeBody, fill, stroke);
    }
}
#endif
