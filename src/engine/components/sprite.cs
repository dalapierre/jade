using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Jade;

class Sprite : Component {
    public bool isInitialized;
    bool visible;
    Renderer renderer;
    Texture2D texture;
    double animationSpeed;
    int imageIndex;
    double elapsedTime;
    Rectangle[] frames;
    Vector2 origin;
    SpriteEffects effect;
    Color color;
    LayerType layer;
    string currentSprite;
    public int width { get { return frames is not null ? (int)(frames[imageIndex].Width * parent.Scale.X): 0; } }
    public int height { get { return frames is not null ? (int)(frames[imageIndex].Height * parent.Scale.Y): 0; } }

    public Sprite(Game game, Entity _parent) : base(_parent) {
        visible = true;
        renderer = game.Services.GetService<Renderer>();
        effect = SpriteEffects.None;
        color = Color.White;
        layer = LayerType.Game;
    }

    public void SetImageIndex(int index) {
        if (index < 0) imageIndex = 0;
        if (index >= frames.Length) imageIndex = frames.Length - 1;
        imageIndex = index;
    }

    public void SetSprite(SpriteData data, LayerType _layer = LayerType.Game) {
        if (data.name != currentSprite) {
            isInitialized = true;
            currentSprite = data.name;
            animationSpeed = data.animationSpeed;
            frames = data.frames;
            texture = data.texture;
            origin = data.origin;
            layer = _layer;
            ResetAnimation();
            GetComponent<Collider>().SetBounds(width, height);
        }
    }

    void ResetAnimation() {
        imageIndex = 0;
    }

    public void SetVisibility(bool _visible) {
        visible = _visible;
    }

    public void SetColor(Color _color) {
        color = _color;
    }

    public void Flip() {
        if (effect == SpriteEffects.None) {
            effect = SpriteEffects.FlipHorizontally;
        } else {
            effect = SpriteEffects.None;
        }
    }

    public override void Update(GameTime gameTime) {
        if (frames is not null && texture is not null && animationSpeed > 0) {
            elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;
            if (elapsedTime >= animationSpeed) {
                ++imageIndex;
                if (imageIndex >= frames.Length) {
                    imageIndex = 0;
                }
                elapsedTime = 0;
            }
        }
    }

    public override void Draw() {
        if (texture is not null && frames is not null && visible) {
            var pos2 = new Vector2(parent.Position.X, parent.Position.Y);
            var frame = frames[imageIndex];
            renderer.DrawTo(layer, texture, pos2, frame, color, parent.Roll, origin, parent.Scale, effect, parent.Position.Z);
        }
    }
}