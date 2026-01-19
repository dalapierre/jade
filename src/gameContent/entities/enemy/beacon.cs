using Jade;
using Microsoft.Xna.Framework;

class Beacon : Entity {
    public bool isDead { get { return GetComponent<BeaconStatsComponent>().isDead; } }
    public Beacon(Game _game) : base(_game) {
        SetSprite("castle");
        AddComponent(new BeaconStatsComponent(this, LevelData.beaconHp));
        AddComponent(new DepthComponent(this));
    }

    public void TakeDamage(int dmg) {
        game.Services.GetService<Mixer>().Play(MixerLayerType.Game, "dmgBeacon");
        GetComponent<BeaconStatsComponent>().TakeDamage(dmg);
    }
}