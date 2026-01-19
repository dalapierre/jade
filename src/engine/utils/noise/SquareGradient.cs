using System;
using Microsoft.Xna.Framework;

namespace Jade;

class SquareGradient {
    public static float GetValue(Point coords, int width, int height, float scale) {
        int halfWidth = width / 2;
        int halfHeight = height / 2;

        int x = coords.X;
        int y = coords.Y;

        Vector2 cv = new Vector2();

        cv = (x < halfWidth) ? new Vector2(width - x, cv.Y) : new Vector2(x, cv.Y);
        cv = (y < halfHeight) ? new Vector2(cv.X, height - y) : new Vector2(cv.X, y);

        cv = new Vector2(cv.X / width, cv.Y / height);

        var val = Math.Max(cv.X, cv.Y);
        val *= (float)Math.Pow(val, scale);

        return val;
    }
}