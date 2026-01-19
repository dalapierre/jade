using System;
using Jade;
using Microsoft.Xna.Framework;

class Blood : Entity {
    double animationTime;
    double elapseTime;
    Vector2 dir;
    float speed;
    public Blood(Game _game, Color color, double _animationTime) : base(_game) {
        animationTime = _animationTime;
        SetSprite("blood_part");
        GetComponent<Sprite>().SetColor(color);
        AddComponent(new DepthComponent(this, 4));
        var gen = _game.Services.GetService<Generator>();
        speed = gen.Next(10, 15);
        dir = new Vector2((float)gen.NextDouble(-1, 1), (float)gen.NextDouble(-1, 1));
        dir.Normalize();
    }

    public override void Update(GameTime gameTime) {
        elapseTime += gameTime.ElapsedGameTime.TotalSeconds;
        if (elapseTime > animationTime) {
            Dispose();
        }

        var xx = dir.X * speed * Math.Pow(gameTime.ElapsedGameTime.TotalSeconds / elapseTime, 2);
        var yy = dir.Y * speed * Math.Pow(gameTime.ElapsedGameTime.TotalSeconds / elapseTime, 2);

        Move((float)xx, (float)yy);

        base.Update(gameTime);
    }
}