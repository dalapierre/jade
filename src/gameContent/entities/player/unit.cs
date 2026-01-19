using Jade;
using Microsoft.Xna.Framework;

enum UnitState {
    Alive,
    Dead
}

class Unit : Entity {
    const double ANIMATION_TIME = 1.5f;
    double elapsedTime;
    GameController controller;
    public UnitStatsComponent stats { get; private set; }
    UnitState state;

    public Unit(Game _game, UnitStatsComponent _stats, string sprite, GameController _controller) : base(_game) {
        stats = _stats;
        SetSprite(sprite);
        AddComponent(new PathMoveComponent(this, stats.speed));
        AddComponent(new DepthComponent(this));
        controller = _controller;
        state = UnitState.Alive;
    }

    public void TakeDamage(int damage) {
        stats.TakeDamage(damage);
    }

    public override void Update(GameTime gameTime) {
        switch (state) {
            case UnitState.Alive:
                AliveLoop();
                break;
            case UnitState.Dead:
                DeadLoop(gameTime);
                break;
        }
        base.Update(gameTime);
    }

    void AliveLoop() {
        var collider = GetComponent<Collider>();
        var other = collider.CheckCollision<Beacon>();
        if (other is not null) {
            other.TakeDamage(stats.damage);
            if (other.isDead) other.Dispose();
            controller.IncreaseDeathCount();
            Dispose();
        }
        if (stats.isDead) {
            var partCount = game.Services.GetService<Generator>().Next(15, 25);
            for (int i = 0; i < partCount; i++) {
                game.Services.GetService<SceneManager>().CreateEntity(new Blood(game, stats.color, ANIMATION_TIME), (int)position.X, (int)position.Y);
            }
            GetComponent<Sprite>().SetVisibility(false);
            game.Services.GetService<Mixer>().Play(MixerLayerType.Game, "unitDead");
            state = UnitState.Dead;
        }
    }

    void DeadLoop(GameTime gameTime) {
        elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;
        if (elapsedTime > ANIMATION_TIME) {
            Dispose();
            controller.IncreaseDeathCount();
        }
    }
}