#if UNITY_EDITOR
using UnityEngine;
using System;
using System.Collections.Generic;
using Chess2DGen;

public static class KnightDrawer
{
    public static Texture2D Draw(Color fill, Color stroke)
    {
        var t = PieceDrawUtil.MakeBlank(); int cx = PieceDrawUtil.TEX / 2;
        var poly = new List<Vector2Int>{
      new(cx-150,230), new(cx+130,230), new(cx+90,320), new(cx+40,360),
      new(cx-15,420),  new(cx-70,400),  new(cx-45,350), new(cx-110,315)
    };
        var big = PieceDrawUtil.Inflate(poly, PieceDrawUtil.STROKE);
        PieceDrawUtil.DrawPolygon(t, big, stroke);
        PieceDrawUtil.DrawPolygon(t, poly, fill);
        PieceDrawUtil.FillCircle(t, cx, 380, 10, (fill == Color.white) ? new Color(.1f, .1f, .12f) : Color.white);
        t.Apply(); return t;
    }
}
#endif
