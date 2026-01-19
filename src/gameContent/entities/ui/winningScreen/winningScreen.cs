using Jade;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class WinningScreen : Entity {
    Renderer renderer;
    SpriteFont font;
    string title;
    Vector2 origin;
    int size;
    
    public WinningScreen(Game _game) : base(_game) {
        renderer = _game.Services.GetService<Renderer>();
        font = _game.Content.Load<SpriteFont>("fonts/MainFont");
        title = "You Won";
        size = 36 / 12;
        origin = font.MeasureString(title) * 0.5f * size;
    }

    public override void Draw() {

        var xx = position.X - origin.X;
        var yy = position.Y - origin.Y;

        float outlineWidth = 4; // Adjust the width as needed

        for (float offsetX = -outlineWidth; offsetX <= outlineWidth; offsetX += 1.0f)
        {
            for (float offsetY = -outlineWidth; offsetY <= outlineWidth; offsetY += 1.0f)
            {
                Vector2 offset = new Vector2(offsetX, offsetY);
                renderer.DrawStringTo(LayerType.UI, font, title, new Vector2(xx + offset.X, yy + offset.Y), Color.Black, 0, Vector2.Zero, size * 12, SpriteEffects.None, 0.5f);
            }
        }

        renderer.DrawStringTo(LayerType.UI, font, title, new Vector2(xx, yy), Color.White, 0, Vector2.Zero, size * 12, SpriteEffects.None, 0);
        base.Draw();
    }
}