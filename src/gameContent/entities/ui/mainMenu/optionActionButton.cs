using System;
using System.Collections.Generic;
using Jade;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class OptionActionButton : Entity {
    Renderer renderer;
    SpriteFont font;
    string text;
    public EventHandler handler { get; set; }
    int size;
    bool selected;
    Texture2D texture;
    Rectangle[] frames;
    string[] options;
    public int currentIndex { get; private set; }

    public OptionActionButton(Game _game, string _text, string[] _options) : base(_game) {
        currentIndex = 0;
        options = _options;
        renderer = _game.Services.GetService<Renderer>();
        font = _game.Content.Load<SpriteFont>("fonts/MainFont");
        text = _text;

        size = 24 / 12;

        var data = _game.Services.GetService<SpriteLoader>().Get("select_icon");
        texture = data.texture;
        frames = data.frames;
    }

    public void SetIndex(int index) {
        currentIndex = index;
    }

    public void IncreaseOption() {
        ++currentIndex;
        if (currentIndex >= options.Length) {
            currentIndex = 0;
        }
    }

    public void Select() { selected = true; }

    public void Unselect() { selected = false; }

    public override void Draw() {
        if (selected) {
            var offset = 12 * scale.Y;
            var xOffset = 64 * scale.X;
            renderer.DrawTo(LayerType.UI, texture, new Vector2(position.X - xOffset, position.Y - offset), frames[0], Color.White, 0, Vector2.Zero, new Vector2(3, 3), SpriteEffects.None, 1);
        }

        renderer.DrawStringTo(LayerType.UI, font, text, new Vector2(position.X, position.Y), Color.White, 0, Vector2.Zero, size * 12, SpriteEffects.None, 0);
        var firstMsgOffset = font.MeasureString(text) * size;
        var msg = options.Length > 0 ? options[currentIndex] : "";
        renderer.DrawStringTo(LayerType.UI, font, msg, new Vector2(position.X + firstMsgOffset.X, position.Y), Color.White, 0, Vector2.Zero, size * 12, SpriteEffects.None, 0);
        base.Draw();
    }
}