using Jade;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class HowToPlay : Entity {
    Renderer renderer;
    SpriteFont font;
    int offsetX;
    int offsetY;

    public HowToPlay(Game _game) : base(_game) {
        renderer = _game.Services.GetService<Renderer>();
        font = _game.Content.Load<SpriteFont>("fonts/MainFont");
        offsetX = _game.GraphicsDevice.Viewport.Width / 16;
        offsetY = _game.GraphicsDevice.Viewport.Height / 16;
    }

    public override void Draw() {
        renderer.DrawStringTo(LayerType.UI, font, "How To Play", new Vector2(offsetX, offsetY), Color.White, 0, Vector2.Zero, 24, SpriteEffects.None, 0);
        renderer.DrawStringTo(LayerType.UI, font, "- Buy units to add them to the queue", new Vector2(offsetX, offsetY * 2), Color.White, 0, Vector2.Zero, 12, SpriteEffects.None, 0);
        renderer.DrawStringTo(LayerType.UI, font, "- Press [Backspace] to remove a unit from the queue", new Vector2(offsetX, offsetY * 2.5f), Color.White, 0, Vector2.Zero, 12, SpriteEffects.None, 0);
        renderer.DrawStringTo(LayerType.UI, font, "- Start the round by pressing ENTER", new Vector2(offsetX, offsetY * 3f), Color.White, 0, Vector2.Zero, 12, SpriteEffects.None, 0);
        renderer.DrawStringTo(LayerType.UI, font, "- Hopefully you destroy the beacon", new Vector2(offsetX, offsetY * 3.5f), Color.White, 0, Vector2.Zero, 12, SpriteEffects.None, 0);
        renderer.DrawStringTo(LayerType.UI, font, "- Get more ressource after every round", new Vector2(offsetX, offsetY * 4f), Color.White, 0, Vector2.Zero, 12, SpriteEffects.None, 0);
        renderer.DrawStringTo(LayerType.UI, font, "- Choose a new effect after every round", new Vector2(offsetX, offsetY * 4.5f), Color.White, 0, Vector2.Zero, 12, SpriteEffects.None, 0);
        renderer.DrawStringTo(LayerType.UI, font, "- Mouse over anything for explanation", new Vector2(offsetX, offsetY * 5f), Color.White, 0, Vector2.Zero, 12, SpriteEffects.None, 0);
        renderer.DrawStringTo(LayerType.UI, font, "- The game ends when you fail to destroy the beacon", new Vector2(offsetX, offsetY * 5.5f), Color.White, 0, Vector2.Zero, 12, SpriteEffects.None, 0);

        renderer.DrawStringTo(LayerType.UI, font, "Keybinds", new Vector2(offsetX, offsetY * 7f), Color.White, 0, Vector2.Zero, 24, SpriteEffects.None, 0);
        renderer.DrawStringTo(LayerType.UI, font, "Q:         Add Striker unit to the queue", new Vector2(offsetX, offsetY * 8f), Color.White, 0, Vector2.Zero, 12, SpriteEffects.None, 0);
        renderer.DrawStringTo(LayerType.UI, font, "W:         Add Seninel unit to the queue", new Vector2(offsetX, offsetY * 8.5f), Color.White, 0, Vector2.Zero, 12, SpriteEffects.None, 0);
        renderer.DrawStringTo(LayerType.UI, font, "E:         Add Bulwark unit to the queue", new Vector2(offsetX, offsetY * 9f), Color.White, 0, Vector2.Zero, 12, SpriteEffects.None, 0);
        renderer.DrawStringTo(LayerType.UI, font, "Enter:     Start the round", new Vector2(offsetX, offsetY * 9.5f), Color.White, 0, Vector2.Zero, 12, SpriteEffects.None, 0);
        renderer.DrawStringTo(LayerType.UI, font, "Backspace: Remove last unit from the queue", new Vector2(offsetX, offsetY * 10f), Color.White, 0, Vector2.Zero, 12, SpriteEffects.None, 0);
        renderer.DrawStringTo(LayerType.UI, font, "Escape:    End game", new Vector2(offsetX, offsetY * 10.5f), Color.White, 0, Vector2.Zero, 12, SpriteEffects.None, 0);

        renderer.DrawStringTo(LayerType.UI, font, "Press [Escape] to go back to the main menu", new Vector2(offsetX, offsetY * 14f), Color.White, 0, Vector2.Zero, 12, SpriteEffects.None, 0);
        base.Draw();
    }
}