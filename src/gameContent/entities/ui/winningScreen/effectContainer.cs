using System;
using Jade;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

enum StatType {
    Damage,
    Health,
    Speed,
    Cost
}

class EffectContainer : Entity {
    const int COST_REDUCTION_ODDS = 20;
    Renderer renderer;
    Texture2D texture;
    Rectangle[] frames;
    Texture2D texture2;
    Rectangle[] frames2;
    Generator gen;
    UnitType type;
    int statIncrease;
    StatType statType;
    string statString;
    string unitType;

    public EffectContainer(Game _game) : base(_game) {
        gen = _game.Services.GetService<Generator>();
        SetScale(6);
        SetSprite("effect_container", LayerType.UI);
        SetDepth(10000);
        renderer = _game.Services.GetService<Renderer>();

        var data = _game.Services.GetService<SpriteLoader>().Get("unit_resource");
        texture = data.texture;
        frames = data.frames;

        var data2 = _game.Services.GetService<SpriteLoader>().Get("stats_icons");
        texture2 = data2.texture;
        frames2 = data2.frames;

        InitStatsIncrease();
        var hover = new SelectMessageComponent(game, this);
        if (statType == StatType.Cost) {
            hover.SetMessage($"{statString}", $"Increase {statString.ToLower()} by {statIncrease}");
        } else {
            hover.SetMessage($"{statString}", $"Increase {unitType}'s {statString.ToLower()} by {statIncrease}");
        }
        hover.SetMessageSize(12, 8);
        AddComponent(hover);
    }

    void InitStatsIncrease() {
        var randId = gen.Next(0, 3);
        switch (randId) {
            case 0:
                type = UnitType.Fast;
                unitType = "Striker";
                break;
            case 1:
                type = UnitType.Regular;
                unitType = "Sentinel";
                break;
            case 2:
                type = UnitType.Slow;
                unitType = "Bulwark";
                break;
        }

        var isCost = gen.Next(100) < COST_REDUCTION_ODDS;
        if (!isCost) {
            var randStat = gen.Next(0, 3);
            switch(randStat) {
                case 0:
                    statType = StatType.Damage;
                    statString = "Damage";
                    break;
                case 1:
                    statType = StatType.Health;
                    statString = "Health";
                    break;
                case 2:
                    statType = StatType.Speed;
                    statString = "Speed";
                    break;
            }
        } else {
            statType = StatType.Cost;
            statString = "Ressources";
        }
        
        switch(statType) {
            case StatType.Damage:
                statIncrease = gen.Next(1, 2);
                break;
            case StatType.Health:
                statIncrease = gen.Next(1, 2);
                break;
            case StatType.Speed:
                statIncrease = gen.Next(10, 25);
                break;
            case StatType.Cost:
                statIncrease = gen.Next(25, LevelData.RESSOURCE_PER_ROUND);
                break;
        }
    }

    public void Handle(Player player) {
        switch (statType) {
            case StatType.Damage:
                UnitDataHelper.IncreaseDamage(type, statIncrease);
                break;
            case StatType.Health:
                UnitDataHelper.IncreaseHP(type, statIncrease);
                break;
            case StatType.Speed:
                UnitDataHelper.IncreaseSpeed(type, statIncrease);
                break;
            case StatType.Cost:
                player.AddRessource(statIncrease);
                break;
        }
        game.Services.GetService<Mixer>().Play(MixerLayerType.Game, "effectSelect");
    }

    public void Select() {
        GetComponent<Sprite>().SetImageIndex(1);
        GetComponent<SelectMessageComponent>().SetSelected(true);
    }

    public void Unselect() {
        GetComponent<Sprite>().SetImageIndex(0);
        GetComponent<SelectMessageComponent>().SetSelected(false);
    }

    public override void Draw() {
        int id = 0;
        switch (type) {
            case UnitType.Fast:
                id = 0;
                break;
            case UnitType.Regular:
                id = 1;
                break;
            case UnitType.Slow:
                id = 2;
                break;
        }
        var xx = GetComponent<Sprite>().width / 2 - frames[id].Width * scale.X / 2;
        var yy = 2 * scale.Y;

        if (statType != StatType.Cost) renderer.DrawTo(LayerType.UI, texture, new Vector2(position.X + xx, position.Y + yy), frames[id], Color.White, 0, Vector2.Zero, Scale, SpriteEffects.None, -1);

        int statsId = 0;
        switch (statType) {
            case StatType.Health:
                statsId = 0;
                break;
            case StatType.Damage:
                statsId = 1;
                break;
            case StatType.Speed:
                statsId = 2;
                break;
            case StatType.Cost:
                statsId = 3;
                break;
        }

        if (statType != StatType.Cost) {
            yy = frames2[statsId].Height * scale.Y;
        } else {
            yy = GetComponent<Sprite>().height / 2 - frames2[statsId].Height * scale.Y / 2;
        }

        renderer.DrawTo(LayerType.UI, texture2, new Vector2(position.X + xx, position.Y + yy), frames2[statsId], Color.White, 0, Vector2.Zero, Scale, SpriteEffects.None, -1);
        base.Draw();
    }
}