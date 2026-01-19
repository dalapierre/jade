using System.Runtime.InteropServices;
using Jade;
using Microsoft.Xna.Framework;

class HowToPlayScene : Scene
{
    public HowToPlayScene(Game _game) : base(_game) {
    }

    public override void Init() {
        SetCamera(new Camera2D(game, 640, 360));
        CreateEntity(new HowToPlayController(game), 0, 0);
    }
}