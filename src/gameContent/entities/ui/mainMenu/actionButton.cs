using System;
using Jade;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class ActionButton : Entity {
    Renderer renderer;
    SpriteFont font;
    string text;
    public EventHandler handler { get; set; }
    int size;
    Vector2 origin;
    bool selected;
    Texture2D texture;
    Rectangle[] frames;

    public ActionButton(Game _game, string _text) : base(_game) {
        renderer = _game.Services.GetService<Renderer>();
        font = _game.Content.Load<SpriteFont>("fonts/MainFont");
        text = _text;

        size = 24 / 12;
        origin = font.MeasureString(text) * 0.5f * size;

        var data = _game.Services.GetService<SpriteLoader>().Get("select_icon");
        texture = data.texture;
        frames = data.frames;
    }

    public void Select() { selected = true; }

    public void Unselect() { selected = false; }

    public override void Draw() {
        if (selected) {
            var offset = 24 * scale.Y;
            var xOffset = 64 * scale.X;
            renderer.DrawTo(LayerType.UI, texture, new Vector2(position.X - origin.X - xOffset, position.Y - offset), frames[0], Color.White, 0, Vector2.Zero, new Vector2(3, 3), SpriteEffects.None, 1);
        }

        renderer.DrawStringTo(LayerType.UI, font, text, new Vector2(position.X - origin.X, position.Y - origin.Y), Color.White, 0, Vector2.Zero, size * 12, SpriteEffects.None, 0);
        base.Draw();
    }
}