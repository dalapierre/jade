using Jade;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

class UndoButton : Entity {

    const float ANIMATION_TIME = 0.25f;
    double elapsedTime;
    Renderer renderer;
    SpriteFont font;
    Keys key;
    UnitContainerState state;
    float speed;

    public UndoButton(Game _game, Keys _key) : base(_game) {
        SetScale(6);
        SetSprite("undo_btn", LayerType.UI);
        renderer = _game.Services.GetService<Renderer>();
        font = _game.Content.Load<SpriteFont>("fonts/MainFont");
        key = _key;
        state = UnitContainerState.AnimateIn;
        var destinationX = _game.GraphicsDevice.Viewport.Width / 9;
        speed = destinationX / ANIMATION_TIME;

        // init hover msg
        var hover = new HoverMessageComponent(_game, this);
        hover.SetMessage("Undo", "Remove the last unit added to the queue", "whenever you press BACKSPACE");
        hover.SetMessageSize(12, 8, 8, 8);
        AddComponent(hover);
        
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

    public void AnimateIn(GameTime gameTime) {
        var deltaT = gameTime.ElapsedGameTime.TotalSeconds;
        elapsedTime += deltaT;
        if (elapsedTime >= ANIMATION_TIME) {
            elapsedTime = 0;
            state = UnitContainerState.Play;
        }

        Move(-speed * (float)deltaT, 0);
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

    public void Play() {
        if (Jade.Keyboard.KeyDown(key)) {
            GetComponent<Sprite>().SetImageIndex(1);
        } else {
            GetComponent<Sprite>().SetImageIndex(0);
        }
    }

    public void Close() {
        state = UnitContainerState.AnimateOut;
    }

    public override void Draw(){
        var xx = position.X + 2 * scale.X;
        var yy = position.Y + 2 * scale.Y;

        var size = 8;
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
}