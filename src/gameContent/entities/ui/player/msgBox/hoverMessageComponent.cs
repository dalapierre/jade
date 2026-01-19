using Jade;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class HoverMessageComponent : Component {
    const int SCALE = 6;
    Renderer renderer;
    bool shouldDraw;
    Texture2D texture;
    Rectangle[] frames;
    Vector2 position;
    string firstRowMsg;
    int firstRowSize;
    string secondRowMsg;
    int secondRowSize;
    string thirdRowMsg;
    int thirdRowSize;
    string fourthRowMSg;
    int fourthRowSize;
    SpriteFont font;
    MessageBoxIconData icon1;
    MessageBoxIconData icon2;
    MessageBoxIconData icon3;
    MessageBoxIconData icon4;

    public HoverMessageComponent(Game game, Entity _parent) : base(_parent) {
        renderer = game.Services.GetService<Renderer>();
        var spriteData = game.Services.GetService<SpriteLoader>().Get("msg_box");
        font = game.Content.Load<SpriteFont>("fonts/MainFont");
        texture = spriteData.texture;
        frames = spriteData.frames;
        var mBoxW = frames[0].Width * SCALE;
        var mBoxH = frames[0].Height * SCALE;
        var mBoxHOffset = 24;

        var xx = game.GraphicsDevice.Viewport.Width / 2 - mBoxW / 2;
        var yy = game.GraphicsDevice.Viewport.Height - mBoxH - mBoxHOffset;

        position = new Vector2(xx, yy);

        firstRowMsg = "";
        firstRowSize = 12;
        secondRowMsg = "";
        secondRowSize = 12;
        thirdRowMsg = "";
        thirdRowSize = 12;
        fourthRowMSg = "";
        fourthRowSize = 12;
    }

    public void SetMessage(string first, string second = "", string third = "", string fourth = "") {
        firstRowMsg = first;
        secondRowMsg = second;
        thirdRowMsg = third;
        fourthRowMSg = fourth;
    }

    public void SetMessageSize(int first, int second = 12, int third = 12, int fourth = 12) {
        firstRowSize = first;
        secondRowSize = second;
        thirdRowSize = third;
        fourthRowSize = fourth;
    }

    public void SetIcons(MessageBoxIconData i1, MessageBoxIconData i2 = null, MessageBoxIconData i3 = null, MessageBoxIconData i4 = null) {
        icon1 = i1;
        icon2 = i2;
        icon3 = i3;
        icon4 = i4;
    }

    public override void Update(GameTime gameTime) {
        var collider = GetComponent<Collider>();
        if (collider.bounds.Contains(Mouse.Pos)) {
            shouldDraw = true;
        } else {
            shouldDraw = false;
        }
    }

    public override void Draw() {
        if (shouldDraw) {
            // draw the container
            renderer.DrawTo(LayerType.UI, texture, position, frames[0], Color.White, 0, Vector2.Zero, new Vector2(SCALE, SCALE), SpriteEffects.None, 1);

            // draw the text
            var textXOffset = 4 * SCALE;
            var textYOffset = 4 * SCALE;

            var bonusOffset = 2 * SCALE;

            renderer.DrawStringTo(LayerType.UI, font, firstRowMsg, position + new Vector2(textXOffset, textYOffset), Color.White, 0, Vector2.Zero, firstRowSize, SpriteEffects.None, 0.4f);
            renderer.DrawStringTo(LayerType.UI, font, secondRowMsg, position + new Vector2(textXOffset, 2 * textYOffset + bonusOffset), Color.White, 0, Vector2.Zero, secondRowSize, SpriteEffects.None, 0.4f);
            renderer.DrawStringTo(LayerType.UI, font, thirdRowMsg, position + new Vector2(textXOffset, 3 * textYOffset + bonusOffset), Color.White, 0, Vector2.Zero, thirdRowSize, SpriteEffects.None, 0.4f);
            renderer.DrawStringTo(LayerType.UI, font, fourthRowMSg, position + new Vector2(textXOffset, 4 * textYOffset + bonusOffset), Color.White, 0, Vector2.Zero, fourthRowSize, SpriteEffects.None, 0.4f);

            var iconSpace = (texture.Width - 2 * textXOffset) / 6 * SCALE;
            var offset = 32 + SCALE;

            // draw the icons
            if (icon1 is not null) {
                var xx = position.X + textXOffset;
                var yy = position.Y + texture.Height - textYOffset;

                renderer.DrawTo(LayerType.UI, texture, new Vector2(xx, yy), icon1.data.frames[icon1.index], Color.White, 0, icon1.data.origin, new Vector2(2, 2), SpriteEffects.None, 0.5f);
                renderer.DrawStringTo(LayerType.UI, font, icon1.text, new Vector2(xx + offset, yy + 12), Color.White, 0, Vector2.Zero, 8, SpriteEffects.None, 0.4f);
            }

            if (icon2 is not null) {
                var xx = position.X + textXOffset + 1 * iconSpace;
                var yy = position.Y + texture.Height - textYOffset;
                
                renderer.DrawTo(LayerType.UI, texture, new Vector2(xx, yy), icon2.data.frames[icon2.index], Color.White, 0, icon2.data.origin, new Vector2(2, 2), SpriteEffects.None, 0.5f);
                renderer.DrawStringTo(LayerType.UI, font, icon2.text, new Vector2(xx + offset, yy + 12), Color.White, 0, Vector2.Zero, 8, SpriteEffects.None, 0.4f);
            }

            if (icon3 is not null) {
                var xx = position.X + textXOffset + 2 * iconSpace;
                var yy = position.Y + texture.Height - textYOffset;

                renderer.DrawTo(LayerType.UI, texture, new Vector2(xx, yy), icon3.data.frames[icon2.index], Color.White, 0, icon3.data.origin, new Vector2(2, 2), SpriteEffects.None, 0.5f);
                renderer.DrawStringTo(LayerType.UI, font, icon3.text, new Vector2(xx + offset, yy + 12), Color.White, 0, Vector2.Zero, 8, SpriteEffects.None, 0.4f);
            }

            if (icon4 is not null) {
                var xx = position.X + textXOffset + 3 * iconSpace;
                var yy = position.Y + texture.Height - textYOffset;

                renderer.DrawTo(LayerType.UI, texture, new Vector2(xx, yy), icon4.data.frames[icon3.index], Color.White, 0, icon4.data.origin, new Vector2(2, 2), SpriteEffects.None, 0.5f);
                renderer.DrawStringTo(LayerType.UI, font, icon4.text, new Vector2(xx + offset, yy + 12), Color.White, 0, Vector2.Zero, 8, SpriteEffects.None, 0.4f);
            }
        }
    }
}