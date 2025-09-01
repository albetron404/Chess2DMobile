#if UNITY_EDITOR
using UnityEngine;
using System;
using Chess2DGen;

public static class KingDrawer
{
    public static Texture2D Draw(Color fill, Color stroke)
    {
        var t = PieceDrawUtil.MakeBlank(); int cx = PieceDrawUtil.TEX / 2;
        Action<Color> fillBody = (c) => {
            PieceDrawUtil.FillEllipse(t, cx, 300, 160, 140, c);
            PieceDrawUtil.FillRect(t, cx - 180, 220, 360, 40, c);
            PieceDrawUtil.FillRect(t, cx - 20, 420, 40, 140, c);   // cross vertical
            PieceDrawUtil.FillRect(t, cx - 90, 470, 180, 30, c);   // cross horizontal
        };
        Action<Color> strokeBody = (c) => {
            PieceDrawUtil.FillEllipse(t, cx, 300, 160 + PieceDrawUtil.STROKE, 140 + PieceDrawUtil.STROKE, c);
            PieceDrawUtil.FillRect(t, cx - (180 + PieceDrawUtil.STROKE), 220 - PieceDrawUtil.STROKE, 360 + 2 * PieceDrawUtil.STROKE, 40 + PieceDrawUtil.STROKE, c);
            PieceDrawUtil.FillRect(t, cx - (20 + PieceDrawUtil.STROKE / 2), 420 - PieceDrawUtil.STROKE, 40 + PieceDrawUtil.STROKE, 140 + PieceDrawUtil.STROKE, c);
            PieceDrawUtil.FillRect(t, cx - 90, 470, 180, 30 + PieceDrawUtil.STROKE, c);
        };
        return PieceDrawUtil.WithStroke(t, fillBody, strokeBody, fill, stroke);
    }
}
#endif
