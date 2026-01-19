using System;
using Jade;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

enum UnitContainerState {
    AnimateIn,
    Play,
    AnimateOut
}

class UnitContainer : Entity {
    const float ANIMATION_TIME = 0.25f;
    double elapsedTime;
    Renderer renderer;
    UnitContainerState state;
    Keys key;
    SpriteFont font;
    int imageIndex;
    Texture2D texture;
    Rectangle[] frames;
    float speed;

    public UnitContainer(Game _game, Keys _key, UnitType _type) : base(_game) {
        imageIndex = (int)_type;
        renderer = _game.Services.GetService<Renderer>();
        font = _game.Content.Load<SpriteFont>("fonts/MainFont");
        key = _key;
        SetScale(6);
        SetSprite("icon_container", LayerType.UI);
        InitHoverMsg(_game, _type);
        state = UnitContainerState.AnimateIn;
        var data = _game.Services.GetService<SpriteLoader>().Get("unit_portraits");
        texture = data.texture;
        frames = data.frames;

        var destinationX = _game.GraphicsDevice.Viewport.Width / 9;
        speed = destinationX / ANIMATION_TIME;
    }


    void InitHoverMsg(Game _game, UnitType _type) {

        var hoverMsg = new HoverMessageComponent(_game, this);
        hoverMsg.SetMessageSize(12, 8, 8, 8);

        var ico1 = game.Services.GetService<SpriteLoader>().Get("stats_icons");
        var ico2 = game.Services.GetService<SpriteLoader>().Get("stats_icons");
        var ico3 = game.Services.GetService<SpriteLoader>().Get("stats_icons");
        var ico4 = game.Services.GetService<SpriteLoader>().Get("stats_icons");

        switch(_type) {
            case UnitType.Fast:
                hoverMsg.SetMessage("Striker", "Unit moving at a fast pace", "Has low damage and health", "Adds this unit to the queue");
                hoverMsg.SetIcons(new MessageBoxIconData(ico1, 0, UnitDataHelper.fastUnitStats.health.ToString()),
                                  new MessageBoxIconData(ico2, 1, UnitDataHelper.fastUnitStats.damage.ToString()),
                                  new MessageBoxIconData(ico3, 2, UnitDataHelper.fastUnitStats.speed.ToString()),
                                  new MessageBoxIconData(ico4, 0, UnitDataHelper.fastUnitStats.cost.ToString()));
                break;
            case UnitType.Regular:
                hoverMsg.SetMessage("Sentinel", "Unit moving at a medium pace", "Has medium damage and health", "Adds this unit to the queue");
                hoverMsg.SetIcons(new MessageBoxIconData(ico1, 0, UnitDataHelper.regularUnitStats.health.ToString()),
                                  new MessageBoxIconData(ico2, 1, UnitDataHelper.regularUnitStats.damage.ToString()),
                                  new MessageBoxIconData(ico3, 2, UnitDataHelper.regularUnitStats.speed.ToString()),
                                  new MessageBoxIconData(ico4, 0, UnitDataHelper.regularUnitStats.cost.ToString()));
                break;
            case UnitType.Slow:
                hoverMsg.SetMessage("Bulwark", "Unit moving at a slow pace", "Has high damage and health", "Adds this unit to the queue");
                hoverMsg.SetIcons(new MessageBoxIconData(ico1, 0, UnitDataHelper.slowUnitStats.health.ToString()),
                                  new MessageBoxIconData(ico2, 1, UnitDataHelper.slowUnitStats.damage.ToString()),
                                  new MessageBoxIconData(ico3, 2, UnitDataHelper.slowUnitStats.speed.ToString()),
                                  new MessageBoxIconData(ico4, 0, UnitDataHelper.slowUnitStats.cost.ToString()));
                break;
        }

        AddComponent(hoverMsg);
    }

    public void Close() {
        state = UnitContainerState.AnimateOut;
    }

    public override void Draw(){
        var xx = position.X + 2 * scale.X;
        var yy = position.Y + 2 * scale.Y;

        renderer.DrawTo(LayerType.UI, texture, new Vector2(Position.X, position.Y), frames[imageIndex], Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, -100);

        var size = 10;
        float outlineWidth = 2; // Adjust the width as needed

        for (float offsetX = -outlineWidth; offsetX <= outlineWidth; offsetX += 1.0f)
        {
            for (float offsetY = -outlineWidth; offsetY <= outlineWidth; offsetY += 1.0f)
            {
                Vector2 offset = new Vector2(offsetX, offsetY);
                renderer.DrawStringTo(LayerType.UI, font, $"[{key}]", new Vector2(xx + offset.X, yy + offset.Y), Color.Black, 0, Vector2.Zero, size, SpriteEffects.None, 0.1f);
            }
        }

        renderer.DrawStringTo(LayerType.UI, font, $"[{key}]", new Vector2(xx, yy), Color.White, 0, Vector2.Zero, size, SpriteEffects.None, 0);
        base.Draw();
    }

    public override void Update(GameTime gameTime) {
        switch (state) {
            case UnitContainerState.AnimateIn:
                AnimateIn(gameTime);
                break;
            case UnitContainerState.Play:
                Play();
                break;
            case UnitContainerState.AnimateOut:
                AnimateOut(gameTime);
                break;
        }
        base.Update(gameTime);
    }
    void AnimateIn(GameTime gameTime) {
        var deltaT = gameTime.ElapsedGameTime.TotalSeconds;
        elapsedTime += deltaT;
        if (elapsedTime >= ANIMATION_TIME) {
            elapsedTime = 0;
            state = UnitContainerState.Play;
        }

        Move(-speed * (float)deltaT, 0);
    }

    void Play() {
        if (Jade.Keyboard.KeyDown(key)) {
            // do stuff
            GetComponent<Sprite>().SetImageIndex(1);
        } else if (Jade.Keyboard.KeyUp(key)) {
            GetComponent<Sprite>().SetImageIndex(0);
        }
    }

    void AnimateOut(GameTime gameTime) {
        var deltaT = gameTime.ElapsedGameTime.TotalSeconds;
        elapsedTime += deltaT;
        if (elapsedTime >= ANIMATION_TIME) {
            elapsedTime = 0;
            Dispose();
        }

        Move(speed * (float)deltaT, 0);
    }

}