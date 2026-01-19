using Jade;
using Microsoft.Xna.Framework;

class Floor : Entity {
    const double GRASS_ODDS = 0.1f;
    const double ROCK_ODDS = 0.01f;
    const double MINERAL_ODDS = 0.0025f;
    public Floor(Game game) : base(game) {
        SetSprite("floor", LayerType.Background);
        var gen = game.Services.GetService<Generator>();

        var odds = gen.NextDouble();
        if (odds < ROCK_ODDS) {
            //AddChildren(new Rock(game), (int)Position.X, (int)position.Y);
        } else if (odds < ROCK_ODDS + MINERAL_ODDS) {
            //AddChildren(new Mineral(game), (int)Position.X, (int)position.Y);
        } else if (odds < ROCK_ODDS + MINERAL_ODDS + GRASS_ODDS) {
            GetComponent<Sprite>().SetImageIndex(1);
        }
    }

    public override void Update(GameTime gameTime) {
        foreach (var child in children.Values) {
            child.SetPosition(position.X, position.Y);
        }
        base.Update(gameTime);
    }
}