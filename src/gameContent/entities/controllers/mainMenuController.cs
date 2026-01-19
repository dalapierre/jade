using System;
using System.Collections.Generic;
using Jade;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

class MainMenuController : Entity {
    const double ANIMATION_TIME = 0.75;
    double elapsedTime;
    int uiWidth;
    int uiHeight;
    MainMenuState state;

    List<ActionButton> actions;
    ActionButton play;
    ActionButton settings;
    ActionButton howToPlay;
    ActionButton exit;
    int selectedIndex;

    GameTitle gameTitle;
    MenuBackground background;

    public MainMenuController(Game _game) : base(_game) {

        state = MainMenuState.Idle;

        uiWidth = game.GraphicsDevice.Viewport.Width;
        uiHeight = game.GraphicsDevice.Viewport.Height;

        gameTitle = new GameTitle(game, "ATTACK ON TOWERS");
        background = new MenuBackground(game);

        AddChildren(gameTitle, uiWidth/2, uiHeight/8);
        AddChildren(background, 0, 0);

        InitActions();
        
    }

    void InitActions() {
        selectedIndex = 0;
        actions = new List<ActionButton>();

        play = new ActionButton(game, "Play");
        play.Select();
        play.handler += HandlePlayAction;

        howToPlay = new ActionButton(game, "How To Play");
        howToPlay.handler += HandleH2PAction;

        settings = new ActionButton(game, "Settings");
        settings.handler += HandleSettingsAction;

        exit = new ActionButton(game, "Exit");
        exit.handler += HandleExitAction;

        actions.Add(play);
        actions.Add(howToPlay);
        actions.Add(settings);
        actions.Add(exit);

        var uiWidth = game.GraphicsDevice.Viewport.Width;
        var uiHeight = game.GraphicsDevice.Viewport.Height;

        var offset = uiHeight / 10;

        AddChildren(play, uiWidth / 2, offset * 4);
        AddChildren(howToPlay, uiWidth / 2, offset * 5);
        AddChildren(settings, uiWidth / 2, offset * 6);
        AddChildren(exit, uiWidth / 2, offset * 7);
    }

    void HandlePlayAction(object sender, EventArgs args) {
        background.SetEnding();
        game.Services.GetService<Mixer>().Play(MixerLayerType.Game, "gameStart");
        state = MainMenuState.AnimateOut;
    }

    void HandleH2PAction(object sender, EventArgs args) {
        game.Services.GetService<Mixer>().Play(MixerLayerType.Game, "menuBlip");
        game.Services.GetService<SceneManager>().SetScene<HowToPlayScene>();
    }

    void HandleSettingsAction(object sender, EventArgs args) {
        game.Services.GetService<Mixer>().Play(MixerLayerType.Game, "menuBlip");
        game.Services.GetService<SceneManager>().SetScene<SettingsScene>();
    }

    void HandleExitAction(object sender, EventArgs args) {
        game.Exit();
    }

    public override void Update(GameTime gameTime) {
        switch (state) {
            case MainMenuState.Idle:
                IdleLoop(gameTime);
                break;
            case MainMenuState.AnimateOut:
                AnimateOutLoop(gameTime);
                break;
            case MainMenuState.Close:
                game.Services.GetService<SceneManager>().SetScene<GameScene>();
                break;
        }
        base.Update(gameTime);
    }

    void AnimateOutLoop(GameTime gameTime) {
        elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;
        if (elapsedTime >= ANIMATION_TIME) {
            state = MainMenuState.Close;
        }
    }

    void IdleLoop(GameTime gameTime) {
        if (Jade.Keyboard.KeyPressed(Keys.Down)) {
            actions[selectedIndex].Unselect();
            ++selectedIndex;
            if (selectedIndex >= actions.Count) {
                selectedIndex = 0;
            }
            actions[selectedIndex].Select();
            game.Services.GetService<Mixer>().Play(MixerLayerType.Game, "menuBlip");
        }
        if (Jade.Keyboard.KeyPressed(Keys.Up)) {
            actions[selectedIndex].Unselect();
            --selectedIndex;
            if (selectedIndex < 0) {
                selectedIndex = actions.Count - 1;
            }
            actions[selectedIndex].Select();
            game.Services.GetService<Mixer>().Play(MixerLayerType.Game, "menuBlip");
        }

        if (Jade.Keyboard.KeyPressed(Keys.Enter)) {
            actions[selectedIndex].handler?.Invoke(this, EventArgs.Empty);
        }

        base.Update(gameTime);
    }
}