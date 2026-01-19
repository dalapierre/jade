using Jade;
using Microsoft.Xna.Framework;

class Mineral : Entity {
    public Mineral(Game _game) : base(_game) {
        SetSprite("minerals");
        var id = _game.Services.GetService<Generator>().Next(0, 3);
        GetComponent<Sprite>().SetImageIndex(id);
        AddComponent(new DepthComponent(this));
    }
}