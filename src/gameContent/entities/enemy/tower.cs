using Jade;
using Microsoft.Xna.Framework;

class Tower : Entity {
    SceneManager sm;

    public Tower(Game _game) : base(_game) {
        sm = _game.Services.GetService<SceneManager>();
        SetSprite("tower");
        AddComponent(new TowerStatsComponent(this, 1.5, 1, 200));
        AddComponent(new DepthComponent(this));
    }

    public override void Update(GameTime gameTime) {
        var stats = GetComponent<TowerStatsComponent>();
        if (stats.canShoot) {
            var target = GetTarget();
            if (target is not null) {
                var xx = position.X + GetComponent<Sprite>().width / 2.0f;
                var yy = position.Y + 4 * scale.Y;
                game.Services.GetService<Mixer>().Play(MixerLayerType.Game, "laserShoot");
                AddChildren(new Bullet(game, target, 1), (int)xx, (int)yy);
                stats.Shoot();
            }
        }

        base.Update(gameTime);
    }

    Unit GetTarget() {
        var enemies = sm.GetEntities<Unit>();
        float smallestDistance = GetComponent<TowerStatsComponent>().range;
        Unit target = null;

        foreach (var enemy in enemies) {
            var distance = DistanceTo(enemy.Position);
            if (distance < smallestDistance && !enemy.stats.isDead) {
                smallestDistance = distance;
                target = enemy;
            }
        }

        return target;
    }
}