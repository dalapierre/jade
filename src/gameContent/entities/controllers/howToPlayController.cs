using Jade;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

class HowToPlayController : Entity {
    public HowToPlayController(Game _game) : base(_game) {
        AddChildren(new MenuBackground(game), 0, 0);
        AddChildren(new HowToPlay(game), 0, 0);
    }

    public override void Update(GameTime gameTime) {
        if (Jade.Keyboard.KeyPressed(Keys.Escape)) {
            game.Services.GetService<Mixer>().Play(MixerLayerType.Game, "menuBlip");
            game.Services.GetService<SceneManager>().SetScene<MainMenuScene>();
        }
        base.Update(gameTime);
    }
}