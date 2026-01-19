using Jade;
using Microsoft.Xna.Framework;

class PlayerStatsComponent : Component {
    double timeBetweenSpawn;
    double elapsedTime;
    public bool canSpawn { get; private set; }

    public PlayerStatsComponent(Entity parent) : base(parent) {
        canSpawn = true;
        timeBetweenSpawn = 0.25;
    }

    public void Spawned() { canSpawn = false; }

    public void SetTimeBetweenSpawn(float time) { timeBetweenSpawn = time; }

    public override void Update(GameTime gameTime) {
        if (!canSpawn) {
            elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;
            if (elapsedTime >= timeBetweenSpawn) {
                elapsedTime = 0;
                canSpawn = true;
            }
        }
    }
}