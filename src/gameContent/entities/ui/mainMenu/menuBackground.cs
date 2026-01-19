using System.Dynamic;
using Jade;
using Microsoft.Xna.Framework;

class MenuBackground : Entity {
    const int CELL_DIM = 16;
    MapGenerator mapGenerator;
    SceneManager sm;

    public MenuBackground(Game _game) : base(_game) {
        sm = _game.Services.GetService<SceneManager>();
        var width = sm.GetCamera<Camera2D>().viewW / CELL_DIM;
        var height = sm.GetCamera<Camera2D>().viewH / CELL_DIM + 1;
        mapGenerator = new MapGenerator(game, width, height);
        var gen = _game.Services.GetService<Generator>();
        var map = mapGenerator.GenerateMenuBackground(width, height, gen);
        

        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                var val = map[i, j];
                var xx = sm.GetCamera<Camera2D>().viewX;
                var yy = sm.GetCamera<Camera2D>().viewY;
                AddChildren(new BackgroundTile(_game, val), xx + i * CELL_DIM, yy + j * CELL_DIM);
            }
        }
    }

    public void SetEnding() {
        foreach (var child in children.Values) {
            (child as BackgroundTile).SetEnding();
        }
    }
}