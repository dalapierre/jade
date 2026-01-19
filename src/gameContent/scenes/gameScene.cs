using Microsoft.Xna.Framework;
using Jade;

class GameScene : Scene {
    public GameScene(Game game) : base(game) {
        SetCamera(new Camera2D(game, 640, 360));
    }

    public override void Init() {
        CreateEntity(new GameController(game), 0, 0);
    }
}