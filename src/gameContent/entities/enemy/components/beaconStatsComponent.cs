using Jade;

class BeaconStatsComponent : Component {
    public int maxHealth { get; private set; }
    public int health { get; private set; }
    public bool isDead { get { return health <= 0; } }
    public BeaconStatsComponent(Entity _parent, int hp) : base(_parent) {
        maxHealth = hp;
        health = hp;
    }

    public void TakeDamage(int dmg) {
        if (health >= dmg) health -= dmg;
        else health = 0;
    }
}