using Microsoft.Xna.Framework;
using Jade;

class MainMenuScene : Scene {
    public MainMenuScene(Game game) : base(game) {
        
    }

    public override void Init() {
        SetCamera(new Camera2D(game, 640, 360));
        CreateEntity(new MainMenuController(game), 0, 0);
    }
}