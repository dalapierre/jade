using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Jade;
class WorleyNoise {
    int Pow;
    List<Vector2> Nodes { get; set; }
    float ScaleX { get; set; }
    float ScaleY { get; set; }
    public WorleyNoise(int width, int height, int nbCell, int pow) {
        ScaleX = 1/(float)width;
        ScaleY = 1/(float)height;
        Nodes = new List<Vector2>();
        Pow = pow;
        var rand = new Random();
        while (Nodes.Count < nbCell) {
            var xx = rand.Next(width) * ScaleX;
            var yy = rand.Next(height) * ScaleY;
            Nodes.Add(new Vector2(xx, yy));
        }
    }

    public float GetValue(int i, int j) {
        var pos = new Vector2(i * ScaleX, j * ScaleY);
        float minDist = int.MaxValue;
        Nodes.ForEach(node => {
            var dist = Vector2.Distance(pos, node);
            if (dist < minDist) {
                minDist = dist;
            }
        });
        return (float)Math.Pow(minDist, Pow);
    }
}