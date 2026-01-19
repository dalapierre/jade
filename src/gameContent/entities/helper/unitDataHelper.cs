using Microsoft.Xna.Framework;

class UnitDataHelper {
    public static UnitStatsComponent fastUnitStats { get; private set; }
    public static UnitStatsComponent regularUnitStats { get; private set; }
    public static UnitStatsComponent slowUnitStats { get; private set; }

    static UnitDataHelper() {
        Reset();
    }

    public static void Reset() {
        fastUnitStats = new UnitStatsComponent(2, 100, 1, 10, new Color(130, 237, 162));
        regularUnitStats = new UnitStatsComponent(3, 75, 2, 15, new Color(31, 141, 211));
        slowUnitStats = new UnitStatsComponent(4, 60, 4, 20, new Color(253, 208, 83));   
    }

    public static void IncreaseDamage(UnitType type, int dmg) {
        switch (type) {
            case UnitType.Fast:
                fastUnitStats.damage += dmg;
                break;
            case UnitType.Regular:
                regularUnitStats.damage += dmg;
                break;
            case UnitType.Slow:
                slowUnitStats.damage += dmg;
                break;
        }
    }

    public static void IncreaseSpeed(UnitType type, int spd) {
        switch (type) {
            case UnitType.Fast:
                fastUnitStats.speed += spd;
                break;
            case UnitType.Regular:
                regularUnitStats.speed += spd;
                break;
            case UnitType.Slow:
                slowUnitStats.speed += spd;
                break;
        }
    }

    public static void IncreaseHP(UnitType type, int hp) {
        switch (type) {
            case UnitType.Fast:
                fastUnitStats.maxHealth += hp;
                fastUnitStats.health = fastUnitStats.maxHealth;
                break;
            case UnitType.Regular:
                regularUnitStats.maxHealth += hp;
                regularUnitStats.health = regularUnitStats.maxHealth;
                break;
            case UnitType.Slow:
                slowUnitStats.maxHealth += hp;
                slowUnitStats.health = slowUnitStats.maxHealth;
                break;
        }
    }
}