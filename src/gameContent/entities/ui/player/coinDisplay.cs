using Jade;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class CoinDisplay : Entity {
    Renderer renderer;
    SpriteFont font;
    int currentResource;

    public CoinDisplay(Game _game) : base(_game) {
        currentResource = 0;
        renderer = _game.Services.GetService<Renderer>();
        font = _game.Content.Load<SpriteFont>("fonts/MainFont");
        SetScale(3);
        SetSprite("coins", LayerType.UI);

        // init hover msg
        var hover = new HoverMessageComponent(_game, this);
        hover.SetMessage("Diamonds", "Diamonds owned by the player", "You get diamonds at the end of a round", "and use them on units");
        hover.SetMessageSize(12, 8, 8, 8);
        AddComponent(hover);
    }

    public void SetResource(int resource) { currentResource = resource; }

    public override void Draw() {
        var xx = position.X + GetComponent<Sprite>().width;
        var yy = position.Y + GetComponent<Sprite>().height / 4;
        var size = 14;

        float outlineWidth = scale.X; // Adjust the width as needed

        for (float offsetX = -outlineWidth; offsetX <= outlineWidth; offsetX += 1.0f)
        {
            for (float offsetY = -outlineWidth; offsetY <= outlineWidth; offsetY += 1.0f)
            {
                Vector2 offset = new Vector2(offsetX, offsetY);
                renderer.DrawStringTo(LayerType.UI, font, currentResource.ToString(), new Vector2(xx + offset.X, yy + offset.Y), Color.Black, 0, Vector2.Zero, size, SpriteEffects.None, 1);
            }
        }

        renderer.DrawStringTo(LayerType.UI, font, currentResource.ToString(), new Vector2(xx, yy), Color.White, 0, Vector2.Zero, size, SpriteEffects.None, 0.5f);
        base.Draw();
    }
}