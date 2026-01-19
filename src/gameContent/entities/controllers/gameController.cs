using System;
using System.Collections.Generic;
using System.Linq;
using Jade;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

class GameController : Entity {
    const int CELL_DIM = 16;
    MapGenerator mapGenerator;
    AI ai;
    Player player;
    Beacon beacon;
    GameState state;
    Generator gen;
    SceneManager sm;
    PlayerMenu playerMenu;

    int sentUnits;
    List<ActionButton> actions;
    List<EffectContainer> effects;
    int selectedIndex = 0;
    WinningScreen winningScreen;

    public GameController(Game _game) : base(_game) {
        selectedIndex = 0;
        actions = new List<ActionButton>();
        effects = new List<EffectContainer>();
        gen = _game.Services.GetService<Generator>();
        sm = _game.Services.GetService<SceneManager>();
        InitChildren();
        state = GameState.Init;
        game.Services.GetService<Mixer>().PlaySong("soundtrack");
    }

    void InitChildren() {
        var width = sm.GetCamera<Camera2D>().viewW / CELL_DIM;
        var height = sm.GetCamera<Camera2D>().viewH / CELL_DIM + 1;
        mapGenerator = new MapGenerator(game, width, height);
        ai = new AI(game);
        player = new Player(game, this);
        playerMenu = new PlayerMenu(game);
        AddChildren(mapGenerator, 0, 0);
        AddChildren(ai, 0, 0);
        AddChildren(player, width * CELL_DIM, 0);
        AddChildren(playerMenu, 0, 0);
    }

    void ResetActions() {
        actions.Clear();
        effects.Clear();
        selectedIndex = 0;
    }

    void InitMap() {
        // Get the new path
        (var map, var path) = mapGenerator.GenerateMap();

        ai.ClearChildren();
        player.ClearChildren();

        // Load the towers
        (map, path) = SetGoal(map, path);
        ai.InitiateLevel(LevelData.level, map, path);
        player.InitiateLevel(path);
    }

    (TileType[,], List<Vector2>) SetGoal(TileType[,] map, List<Vector2> path) {
        if (beacon is not null) beacon.Dispose();
        var node = path.Last();

        var neighbors = new List<Vector2>{
            new Vector2(node.X + 1, node.Y),
            new Vector2(node.X - 1, node.Y),
            new Vector2(node.X, node.Y + 1),
            new Vector2(node.X, node.Y - 1)
        };

        var w = map.GetLength(0);
        var h = map.GetLength(1);

        var okay = false;
        while (!okay) {
            var id = gen.Next(neighbors.Count);
            var neighbor = new Vector2((int)(neighbors[id].X / CELL_DIM), (int)(neighbors[id].Y / CELL_DIM));
            if (neighbor.X < w && neighbor.X >= 0 && neighbor.Y < h && neighbor.Y >= 0) {
                if (map[(int)neighbor.X, (int)neighbor.Y] == TileType.Floor) {
                    var xx = sm.viewX + neighbor.X * CELL_DIM;
                    var yy = sm.viewY + neighbor.Y * CELL_DIM;

                    beacon = new Beacon(game);
                    AddChildren(beacon, (int)xx, (int)yy);
                    path.Add(new Vector2(neighbor.X * CELL_DIM, neighbor.Y * CELL_DIM));
                    map[(int)neighbor.X, (int)neighbor.Y] = TileType.castle;
                    okay = true;
                }
            }
        }

        return (map, path);
    }
    
    public void IncreaseDeathCount() {
        --sentUnits;
    }

    public override void Update(GameTime gameTime) {
        switch (state) {
            case GameState.Init:
                InitLoop();
                break;
            case GameState.Choosing:
                ChoosingLoop();
                break;
            case GameState.Playing:
                PlayingLoop();
                break;
            case GameState.Won:
                WonLoop();
                break;
            case GameState.Lost:
                LostLoop();
                break;
        }
        SetUIData();
        if (Jade.Keyboard.KeyPressed(Keys.Escape)) {
            player.ClearChildren();
            ShowLosingScreen();
            game.Services.GetService<Mixer>().Play(MixerLayerType.Game, "gameLost");
            state = GameState.Lost;
        }
        base.Update(gameTime);
    }

    public void SetUIData() {
        playerMenu.SetResources(player.GetComponent<ResourceComponent>().resource,
                                beacon.GetComponent<BeaconStatsComponent>().health,
                                LevelData.level);
        playerMenu.SetUnitsValue(player.units);
    }

    void PlayingLoop() {
        if (beacon.GetComponent<BeaconStatsComponent>().isDead) {
            LevelData.IncreaseLevel();
            player.GetComponent<ResourceComponent>().AddResource(LevelData.RESSOURCE_PER_ROUND);
            player.ClearChildren();
            ShowWinningScreen();
            game.Services.GetService<Mixer>().Play(MixerLayerType.Game, "beaconDead");
            state = GameState.Won;
        } else if (sentUnits <= 0) {
            ShowLosingScreen();
            game.Services.GetService<Mixer>().Play(MixerLayerType.Game, "gameLost");
            state = GameState.Lost;
        }
    }

    void ShowWinningScreen() {
        var uiWidth = game.GraphicsDevice.Viewport.Width;
        var uiHeight = game.GraphicsDevice.Viewport.Height;
        var yOffset = uiHeight / 8;
        var xOffset = uiWidth / 5;
        winningScreen = new WinningScreen(game);
        AddChildren(winningScreen, uiWidth / 2, yOffset);

        for (int i = 0; i < 3; i++) {
            var effect = new EffectContainer(game);
            if (i == 0) effect.Select();
            effects.Add(effect);
            var w = effect.GetComponent<Sprite>().width / 2;
            AddChildren(effect, xOffset * (i + 1) + w, 3 * yOffset);
        }
    }

    void ShowLosingScreen() {
        ClearChildren();
        AddChildren(new MenuBackground(game), 0, 0);

        var uiWidth = game.GraphicsDevice.Viewport.Width;
        var uiHeight = game.GraphicsDevice.Viewport.Height;
        var offset = uiHeight / 8;

        AddChildren(new LostWindow(game), uiWidth / 2, offset);
        AddChildren(new ScoreMessage(game), uiWidth / 2, 3 * offset);

        var goAgain = new ActionButton(game, "Start Again");
        goAgain.handler += HandleStartAgain;
        goAgain.Select();

        var exit = new ActionButton(game, "Go To Menu");
        exit.handler += HandleGoToMenu;

        actions.Add(goAgain);
        actions.Add(exit);

        AddChildren(goAgain, uiWidth / 2, 5 * offset);
        AddChildren(exit, uiWidth / 2, 6 * offset);
    }

    void HandleStartAgain(object sender, EventArgs args) {
        LevelData.Reset();
        UnitDataHelper.Reset();
        ClearChildren();
        InitChildren();
        ResetActions();
        game.Services.GetService<Mixer>().Play(MixerLayerType.Game, "gameStart");
        state = GameState.Init;
    }

    void HandleGoToMenu(object sender, EventArgs args) {
        game.Services.GetService<Mixer>().Stop();
        game.Services.GetService<Mixer>().Play(MixerLayerType.Game, "menuBlip");
        sm.SetScene<MainMenuScene>();
    }

    void LostLoop() {
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
    }

    void WonLoop() {
        if (Jade.Keyboard.KeyPressed(Keys.Right)) {
            effects[selectedIndex].Unselect();
            ++selectedIndex;
            if (selectedIndex >= effects.Count) {
                selectedIndex = 0;
            }
            effects[selectedIndex].Select();
            game.Services.GetService<Mixer>().Play(MixerLayerType.Game, "menuBlip");
        }
        if (Jade.Keyboard.KeyPressed(Keys.Left)) {
            effects[selectedIndex].Unselect();
            --selectedIndex;
            if (selectedIndex < 0) {
                selectedIndex = effects.Count - 1;
            }
            effects[selectedIndex].Select();
            game.Services.GetService<Mixer>().Play(MixerLayerType.Game, "menuBlip");
        }

        if (Jade.Keyboard.KeyPressed(Keys.Enter)) {
            effects[selectedIndex].Handle(player);
            foreach (var effect in effects) {
                effect.Dispose();
            }
            effects.Clear();
            winningScreen.Dispose();
            state = GameState.Init;
            ResetActions();
        }
    }

    void InitLoop() {
        InitMap();
        player.SetState(PlayerState.Choosing);
        state = GameState.Choosing;
        playerMenu.ShowUI();
    }

    void ChoosingLoop() {
        if (Jade.Keyboard.KeyUp(Keys.Enter)) {
            if (player.units.Count > 0) {
                playerMenu.CloseUI();
                state = GameState.Playing;
                sentUnits = player.units.Count;
                player.SetState(PlayerState.Playing);
            } else {
                game.Services.GetService<Mixer>().Play(MixerLayerType.Game, "impossible");
            }
        }
    }
}