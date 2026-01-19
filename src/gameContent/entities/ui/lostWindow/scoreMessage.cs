using Jade;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class ScoreMessage : Entity {
    Renderer renderer;
    SpriteFont font;
    string title;
    Vector2 origin;
    int size;
    
    public ScoreMessage(Game _game) : base(_game) {
        renderer = _game.Services.GetService<Renderer>();
        font = _game.Content.Load<SpriteFont>("fonts/MainFont");
        title = $"You reached level: {LevelData.level}";
        size = 24 / 12;
        origin = font.MeasureString(title) * 0.5f * size;
    }

    public override void Draw() {
        renderer.DrawStringTo(LayerType.UI, font, title, new Vector2(position.X - origin.X, position.Y - origin.Y), Color.White, 0, Vector2.Zero, size * 12, SpriteEffects.None, 0);
        base.Draw();
    }
}