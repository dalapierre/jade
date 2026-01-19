using Jade;
using Microsoft.Xna.Framework;

class SettingsScene : Scene {
    public SettingsScene(Game _game) : base(_game) { }

    public override void Init() {
        SetCamera(new Camera2D(game, 640, 360));
        CreateEntity(new SettingsController(game), 0, 0);
    }
}