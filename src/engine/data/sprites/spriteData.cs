using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Jade;

class SpriteData {
    public double animationSpeed;
    public Rectangle[] frames;
    public Texture2D texture { get; private set; }
    public Vector2 origin { get; private set; }
    public string name { get; private set; }

    public SpriteData(string _name, Texture2D _texture, double _animationSpeed, Rectangle[] _frames, Vector2 _origin) {
        name = _name;
        texture = _texture;
        animationSpeed = _animationSpeed;
        frames = _frames;
        origin = _origin;
    }
}