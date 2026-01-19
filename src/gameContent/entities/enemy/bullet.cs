using Jade;
using Microsoft.Xna.Framework;

class Bullet : Entity {
    Unit target;
    float speed;
    int damage;


    public Bullet(Game _game, Unit _target, int _damage) : base(_game) {
        SetSprite("bullet");
        AddComponent(new DepthComponent(this));
        target = _target;
        speed = 400;
        damage = _damage;
    }

    public override void Update(GameTime gameTime) {
        CheckCollision();
        MoveBullet(gameTime);
        base.Update(gameTime);
    }

    void CheckCollision() {
        var collider = GetComponent<Collider>();
        if (collider.CheckCollision(target)) {
            target.TakeDamage(damage);
            game.Services.GetService<Mixer>().Play(MixerLayerType.Game, "bulletHit");
            Dispose();
        }
    }

    void MoveBullet(GameTime gameTime) {
        var spriteX = target.GetComponent<Sprite>().width / 2;
        var spriteY = target.GetComponent<Sprite>().height / 2;
        var direction = new Vector2(target.Position.X + spriteX, target.Position.Y + spriteY) - new Vector2(position.X, position.Y);
        direction.Normalize();

        var xx = direction.X * speed * gameTime.ElapsedGameTime.TotalSeconds;
        var yy = direction.Y * speed * gameTime.ElapsedGameTime.TotalSeconds;

        Move((float)xx, (float)yy);
    }
}