using System;
using System.Collections.Generic;
using Jade;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

class PlayerMenu : Entity {
    const int OFFSET = 12;
    int uiWidth;
    int uiHeight;

    UnitContainer fastUnit;
    UnitContainer regularUnit;
    UnitContainer slowUnit;
    CoinDisplay coins;
    UnitDisplay fastDisplay;
    UnitDisplay regularDisplay;
    UnitDisplay slowDisplay;
    BeaconHealthDisplay beaconHp;
    LevelDisplay levelDisplay;
    StartButton start;
    UndoButton undo;

    public PlayerMenu(Game _game) : base(_game) {
        coins = new CoinDisplay(_game);
        fastDisplay = new UnitDisplay(game, UnitType.Fast);
        regularDisplay = new UnitDisplay(game, UnitType.Regular);
        slowDisplay = new UnitDisplay(game, UnitType.Slow);
        uiWidth = _game.GraphicsDevice.Viewport.Width;
        uiHeight = _game.GraphicsDevice.Viewport.Height;
        beaconHp = new BeaconHealthDisplay(game);
        levelDisplay = new LevelDisplay(game);

        var xOffset = uiWidth / 9;

        AddChildren(coins, 0, OFFSET);
        AddChildren(fastDisplay, xOffset,OFFSET);
        AddChildren(regularDisplay, 2 * xOffset, OFFSET);
        AddChildren(slowDisplay, 3 * xOffset, OFFSET);
        AddChildren(levelDisplay, 4 * xOffset, OFFSET);
        AddChildren(beaconHp, 5 * xOffset, OFFSET);
    }

    public void SetResources(int _coins, int _beaconHp, int level) {
        coins.SetResource(_coins);
        beaconHp.SetResource(_beaconHp);
        levelDisplay.SetResource(level);
    }

    public void SetUnitsValue(Queue<UnitType> units) {
        fastDisplay.SetResource(units);
        regularDisplay.SetResource(units);
        slowDisplay.SetResource(units);
    }

    public void ShowUI() {
        var OFFSET = 24;
        fastUnit = new UnitContainer(game, Keys.Q, UnitType.Fast);
        regularUnit = new UnitContainer(game, Keys.W, UnitType.Regular);
        slowUnit = new UnitContainer(game, Keys.E, UnitType.Slow);
        start = new StartButton(game, Keys.Enter);
        undo = new UndoButton(game, Keys.Back);

        var h = uiHeight / 6;
        var btnW = start.GetComponent<Sprite>().width;

        AddChildren(fastUnit, uiWidth, h*1);
        AddChildren(regularUnit, uiWidth, h*2);
        AddChildren(slowUnit, uiWidth, h*3);
        AddChildren(start, uiWidth - btnW - OFFSET, h * 5);
        AddChildren(undo, uiWidth, h * 5);
    }

    public void CloseUI() {
        fastUnit.Close();
        regularUnit.Close();
        slowUnit.Close();
        start.Close();
        undo.Close();
    }
}