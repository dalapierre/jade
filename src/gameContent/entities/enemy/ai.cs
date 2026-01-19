using System.Collections.Generic;
using Jade;
using Microsoft.Xna.Framework;

class AI : Entity {
    Generator gen;
    SceneManager sm;
    public AI(Game _game) : base(_game) {
        gen = game.Services.GetService<Generator>();
        sm = game.Services.GetService<SceneManager>();
    }

    public void InitiateLevel(int level, TileType[,] map, List<Vector2> path) {
        CreateTowers(level, map, path);
    }

    void CreateTowers(int level, TileType[,] map, List<Vector2> path) {
        var towerCount = level;
        // Initialize tower
        while (towerCount > 0) {
            var tower = new Tower(game);
            var x = gen.Next(8, map.GetLength(0) - 8);
            var y = gen.Next(8, map.GetLength(1) - 8);

            var found = false;

            while(map[x, y] != TileType.Floor && !found) {

                x = gen.Next(8, map.GetLength(0) - 8);
                y = gen.Next(8, map.GetLength(1) - 8);

                foreach (var node in path) {
                    var dist = Vector2.Distance(new Vector2(x * 16, y * 16), node);
                    if (dist <= tower.GetComponent<TowerStatsComponent>().range / 2) {
                        found = true;
                    }
                }
            }

            map[x, y] = TileType.Tower;

            var xx = sm.GetCamera<Camera2D>().viewX + x * 16;
            var yy = sm.GetCamera<Camera2D>().viewY + y * 16;

            AddChildren(tower, xx, yy);
            --towerCount;
        }
    }
}