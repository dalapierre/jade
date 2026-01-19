using Microsoft.Xna.Framework;

namespace Jade;
class Algo {
    public NoiseType Type { get; private set; }
    public float Roughness { get; private set; }
    public float OptionalParam { get; private set; }
    public Vector2 Center { get; private set; }
    public Vector2 Offset { get; private set; }
    public int Depth { get; private set; }
    public bool Substract { get; private set; }
    public bool Reverse { get; private set; }
    
    public Algo(Generator generator, NoiseType type, float roughness, Vector2 center, int depth, float optionalParam = 1, bool sub = false, bool reverse = false) {
        Type = type;
        Roughness = roughness;
        OptionalParam = optionalParam;
        Offset = new Vector2((float)generator.NextDouble(), (float)generator.NextDouble());
        Center = Offset + center;
        Depth = depth;
        Substract = sub;
        Reverse = reverse;
    }
}