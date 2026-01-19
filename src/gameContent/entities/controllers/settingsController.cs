using System;
using System.Collections.Generic;
using Jade;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

class SettingsController : Entity {
    List<OptionActionButton> actions;
    OptionActionButton backgroundSound;
    OptionActionButton soundEffect;
    OptionActionButton back;
    int selectedIndex;
    public SettingsController(Game _game) : base(_game) {
        Init();
    }

    void Init() {
        selectedIndex = 0;
        actions = new List<OptionActionButton>();

        backgroundSound = new OptionActionButton(game, "Music: ", Settings.BFXVolume);
        backgroundSound.handler += HandleBackgroundVolumeAction;
        backgroundSound.SetIndex(Settings.currentBFXVolume);

        soundEffect = new OptionActionButton(game, "Sound effects: ", Settings.SFXVolume);
        soundEffect.handler += HandleSoundEffectVolumeAction;
        soundEffect.SetIndex(Settings.currentSFXVolume);

        back = new OptionActionButton(game, "Back", new string[0]);

        back.handler += HandleBackAction;

        actions.Add(backgroundSound);
        actions.Add(soundEffect);
        actions.Add(back);

        AddChildren(new MenuBackground(game), 0, 0);

        var uiWidth = game.Services.GetService<GraphicsDeviceManager>().PreferredBackBufferWidth;
        var uiHeight = game.Services.GetService<GraphicsDeviceManager>().PreferredBackBufferHeight;

        var offset = uiHeight / 5;

        AddChildren(backgroundSound, uiWidth / 2 - uiWidth / 6, offset);
        AddChildren(soundEffect, uiWidth / 2 - uiWidth / 6, 2 * offset);
        AddChildren(back, uiWidth / 2 - uiWidth / 6, 4 * offset);

        actions[selectedIndex].Select();
    }

    void HandleBackgroundVolumeAction(object sender, EventArgs args) {
        backgroundSound.IncreaseOption();
        Settings.SetBFXVolume(backgroundSound.currentIndex);
        switch(backgroundSound.currentIndex) {
            case 0:
                game.Services.GetService<Mixer>().SetVolume(MixerLayerType.Background, 1f);
                break;
            case 1:
                game.Services.GetService<Mixer>().SetVolume(MixerLayerType.Background, 0.75f);
                break;
            case 2:
                game.Services.GetService<Mixer>().SetVolume(MixerLayerType.Background, 0.5f);
                break;
            case 3:
                game.Services.GetService<Mixer>().SetVolume(MixerLayerType.Background, 0.25f);
                break;
            case 4:
                game.Services.GetService<Mixer>().SetVolume(MixerLayerType.Background, 0f);
                break;
        }
        game.Services.GetService<Mixer>().Play(MixerLayerType.Background, "menuBlip");
    }

    void HandleSoundEffectVolumeAction(object sender, EventArgs args) {
        soundEffect.IncreaseOption();
        Settings.SetSFXVolume(soundEffect.currentIndex);
        switch(soundEffect.currentIndex) {
            case 0:
                game.Services.GetService<Mixer>().SetVolume(MixerLayerType.Game, 1f);
                break;
            case 1:
                game.Services.GetService<Mixer>().SetVolume(MixerLayerType.Game, 0.75f);
                break;
            case 2:
                game.Services.GetService<Mixer>().SetVolume(MixerLayerType.Game, 0.5f);
                break;
            case 3:
                game.Services.GetService<Mixer>().SetVolume(MixerLayerType.Game, 0.25f);
                break;
            case 4:
                game.Services.GetService<Mixer>().SetVolume(MixerLayerType.Game, 0f);
                break;
        }
        game.Services.GetService<Mixer>().Play(MixerLayerType.Game, "menuBlip");
    }

    void HandleBackAction(object sender, EventArgs args) {
        game.Services.GetService<SceneManager>().SetScene<MainMenuScene>();
        game.Services.GetService<Mixer>().Play(MixerLayerType.Game, "menuBlip");
    }

    public override void Update(GameTime gameTime) {
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