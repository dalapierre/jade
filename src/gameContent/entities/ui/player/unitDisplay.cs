using System.Collections.Generic;
using System.Linq;
using Jade;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class UnitDisplay : Entity {
    Renderer renderer;
    SpriteFont font;
    int currentResource;
    UnitType type;

    public UnitDisplay(Game _game, UnitType _type) : base(_game) {
        type = _type;
        renderer = _game.Services.GetService<Renderer>();
        font = _game.Content.Load<SpriteFont>("fonts/MainFont");
        SetScale(3);
        SetSprite("unit_resource", LayerType.UI);
        GetComponent<Sprite>().SetImageIndex((int)type);

        // init hover msg
        var hover = new HoverMessageComponent(_game, this);
        string unitName = "";
        switch (_type) {
            case UnitType.Fast:
                unitName = "Striker queued";
                break;
            case UnitType.Regular:
                unitName = "Sentinel queued";
                break;
            case UnitType.Slow:
                unitName = "Bulwark queued";
                break;
        }

        hover.SetMessage(unitName, $"Amount of {unitName} currently in", "queue");
        hover.SetMessageSize(12, 8, 8, 8);
        AddComponent(hover);
    }

    public void SetResource(Queue<UnitType> units) {
        currentResource = units.Where(x => x == type).Count();
    }

    public override void Draw() {
        var xx = position.X + GetComponent<Sprite>().width;
        var yy = position.Y + GetComponent<Sprite>().height / 4f;
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