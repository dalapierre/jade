using System;

class LevelData {
    const int INITIAL_HP = 5;
    const float LIMITING_CONST = 0.15f;
    public const int RESSOURCE_PER_ROUND = 125;
    public static int level { get; private set; }
    public static int ressource { get; private set; }
    public static int beaconHp { get; private set; }
    static LevelData() {
        Reset();
    }

    public static void IncreaseLevel() {
        // do some cool stuff and increase level + ressource
        ++level;
        beaconHp = (int)(INITIAL_HP * Math.Pow(Math.E, LIMITING_CONST * level));
    }

    public static void Reset() {
        level = 1;
        ressource = RESSOURCE_PER_ROUND;
        beaconHp = 5;
    }

    public static void IncreaseRessource(int value) {
        ressource += value;
    }
}