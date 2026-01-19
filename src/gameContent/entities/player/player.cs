using System.Collections.Generic;
using System.Linq;
using Jade;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

class Player : Entity {
    List<Vector2> path;
    SceneManager sm;
    PlayerState state;
    public Queue<UnitType> units { get; private set; }
    GameController controller;

    public Player(Game _game, GameController _controller) : base(_game){
        controller = _controller;
        state = PlayerState.Choosing;
        sm = _game.Services.GetService<SceneManager>();
        AddComponent(new ResourceComponent(this));
        AddComponent(new PlayerStatsComponent(this));

        units = new Queue<UnitType>();

        GetComponent<ResourceComponent>().AddResource(LevelData.ressource);
    }

    public void InitiateLevel(List<Vector2> _path) {
        path = _path;
    }

    public void AddRessource(int val) {
        GetComponent<ResourceComponent>().AddResource(val);
    }

    public void SetState(PlayerState _state) { state = _state; }

    public override void Update(GameTime gameTime) {
        // TODO: add player state

        if (Jade.Keyboard.KeyPressed(Keys.F1)) {
            ClearChildren();
        }

        switch(state) {
            case PlayerState.Choosing:
                ChoosingLoop();
                break;
            case PlayerState.Playing:
                PlayingLoop();
                break;
            case PlayerState.Stop:
                StopLoop();
                break;
        }
        base.Update(gameTime);
    }

    public void ChoosingLoop() {
        var res = GetComponent<ResourceComponent>();
        if (Jade.Keyboard.KeyUp(Keys.Q)) {
            if (res.SpendResource(UnitDataHelper.fastUnitStats.cost)) {
                game.Services.GetService<Mixer>().Play(MixerLayerType.Game, "menuBlip");
                units.Enqueue(UnitType.Fast);
            } else {
                game.Services.GetService<Mixer>().Play(MixerLayerType.Game, "impossible");
            }
        }
        if (Jade.Keyboard.KeyUp(Keys.W)) {
            if (res.SpendResource(UnitDataHelper.regularUnitStats.cost)) {
                game.Services.GetService<Mixer>().Play(MixerLayerType.Game, "menuBlip");
                units.Enqueue(UnitType.Regular);
            } else {
                game.Services.GetService<Mixer>().Play(MixerLayerType.Game, "impossible");
            }
        }
        if (Jade.Keyboard.KeyUp(Keys.E)) {
            if (res.SpendResource(UnitDataHelper.slowUnitStats.cost)) {
                game.Services.GetService<Mixer>().Play(MixerLayerType.Game, "menuBlip");
                units.Enqueue(UnitType.Slow);
            } else {
                game.Services.GetService<Mixer>().Play(MixerLayerType.Game, "impossible");
            }
        }
        if (Jade.Keyboard.KeyUp(Keys.Back)) {
            if (units.Count > 0) {
                units = new Queue<UnitType>(units.Reverse());
                var type = units.Dequeue();
                switch (type) {
                    case UnitType.Fast:
                        res.AddResource(UnitDataHelper.fastUnitStats.cost);
                        break;
                    case UnitType.Regular:
                        res.AddResource(UnitDataHelper.regularUnitStats.cost);
                        break;
                    case UnitType.Slow:
                        res.AddResource(UnitDataHelper.slowUnitStats.cost);
                        break;
                }
                units = new Queue<UnitType>(units.Reverse());
                game.Services.GetService<Mixer>().Play(MixerLayerType.Game, "undo");
            } else {
                game.Services.GetService<Mixer>().Play(MixerLayerType.Game, "impossible");
            }
        }
    }

    public void PlayingLoop() {
        var stats = GetComponent<PlayerStatsComponent>();

        if (stats.canSpawn && units.Count > 0) {
            var unitType = units.Dequeue();
            if (path is not null) {
                var startX = sm.viewX + path[0].X;
                var startY = sm.viewY + path[0].Y;
                Unit unit;

                switch (unitType) {
                    case UnitType.Fast:
                        unit = new Unit(game, new UnitStatsComponent(UnitDataHelper.fastUnitStats), "unit_fast", controller);
                        break;
                    case UnitType.Regular:
                        unit = new Unit(game, new UnitStatsComponent(UnitDataHelper.regularUnitStats), "unit_regular", controller);
                        break;
                    default:
                        unit = new Unit(game, new UnitStatsComponent(UnitDataHelper.slowUnitStats), "unit_slow", controller);
                        break;
                }
                
                unit.GetComponent<PathMoveComponent>().SetPath(path);
                AddChildren(unit, (int)startX, (int)startY);
                game.Services.GetService<Mixer>().Play(MixerLayerType.Game, "spawn");
                stats.Spawned();
            }
        }
    }

    public void StopLoop() {

    }
}