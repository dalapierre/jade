using Jade;

class GameApp : App {
    public GameApp() : base() {
        //_graphics.PreferredBackBufferWidth = 1920;
        //_graphics.PreferredBackBufferHeight = 1080;
    }

    public override void Init() {

    }

    public override void RegisterScenes() {
        AddScene(new MainMenuScene(this));
        AddScene(new HowToPlayScene(this));
        AddScene(new SettingsScene(this));
        AddScene(new GameScene(this));
    }
}