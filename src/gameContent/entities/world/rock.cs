using Jade;
using Microsoft.Xna.Framework;

class Rock : Entity {
    public Rock(Game _game) : base(_game) {
        SetSprite("rock");
        var id = _game.Services.GetService<Generator>().Next(0, 4);
        GetComponent<Sprite>().SetImageIndex(id);
        AddComponent(new DepthComponent(this));
    }
}