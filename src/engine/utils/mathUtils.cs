using System;

namespace Jade;

class MathUtils {
    public static float ToRad(float angle) {
        return (float)(angle * (Math.PI / 180));
    }
}