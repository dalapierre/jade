
using Microsoft.Xna.Framework;

class UnitStatsComponent {
    public int cost { get; set; }
    public int maxHealth { get; set; }
    public int health { get; set; }
    public int speed { get; set; }
    public int damage { get; set; }
    public bool isDead { get { return health <= 0; } }
    public Color color { get; private set; }
    public UnitStatsComponent(int hp, int spd, int dmg, int _cost, Color _color) {
        maxHealth = hp;
        health = hp;
        speed = spd;
        damage = dmg;
        cost = _cost;
        color = _color;
    }

    public UnitStatsComponent(UnitStatsComponent copy) {
        maxHealth = copy.health;
        health = maxHealth;
        speed = copy.speed;
        damage = copy.damage;
        color = copy.color;
    }

    public void TakeDamage(int dmg) {
        if (health >= dmg) health -= dmg;
        else health = 0;
    }
}