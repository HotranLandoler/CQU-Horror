using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Direction
{
    public static Vector2 Dir2Vector(int val)
    {
        switch (val)
        {
            case 0:
                return new Vector2(0, -1);
            case 1:
                return new Vector2(1, 0);
            case 2:
                return new Vector2(-1, 0);
            case 3:
                return new Vector2(0, 1);
            default:
                return Vector2.zero;
        }
    }

    public static Vector2 NormalDir(Vector2 val)
    {
        if (val.y <= val.x && val.y >= -val.x)
            return new Vector2(1, 0);
        else if (val.y >= val.x && val.y <= -val.x)
            return new Vector2(-1, 0);
        else if (val.y > 0)
            return new Vector2(0, 1);
        else
            return new Vector2(0, -1);
    }

    public static int NormalDirInt(Vector2 val)
    {
        if (val.y <= val.x && val.y >= -val.x)
            return 1;
        else if (val.y >= val.x && val.y <= -val.x)
            return 2;
        else if (val.y > 0)
            return 3;
        else
            return 0;
    }
}
