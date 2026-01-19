

using Jade;
using Microsoft.Xna.Framework;

class TowerStatsComponent : Component {
    double elapsedTime;
    double attackSpeed;
    public int damage { get; private set; }
    public int range { get; private set; }
    public bool canShoot { get; private set; }

    public TowerStatsComponent(Entity _parent, double _attackSpeed, int dmg, int _range) : base(_parent) {
        canShoot = true;
        attackSpeed = _attackSpeed;
        damage = dmg;
        range = _range;
    }

    public void Shoot() { canShoot = false; }

    public override void Update(GameTime gameTime) {
        if (!canShoot) {
            elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;
            if (elapsedTime >= attackSpeed) {
                canShoot = true;
                elapsedTime = 0;
            }
        }
    }
}