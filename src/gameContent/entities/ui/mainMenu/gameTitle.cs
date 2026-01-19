using Jade;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class GameTitle : Entity {
    Renderer renderer;
    SpriteFont font;
    string title;
    Vector2 origin;
    int size;

    public GameTitle(Game _game, string _title) : base(_game) {
        renderer = _game.Services.GetService<Renderer>();
        font = _game.Content.Load<SpriteFont>("fonts/MainFont");
        title = _title;
        size = 36 / 12;
        origin = font.MeasureString(title) * 0.5f * size;
    }

    public override void Draw() {
        renderer.DrawStringTo(LayerType.UI, font, title, new Vector2(position.X - origin.X, position.Y - origin.Y), Color.White, 0, Vector2.Zero, size * 12, SpriteEffects.None, 0);
        base.Draw();
    }
}