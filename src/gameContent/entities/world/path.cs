using System.Collections.Generic;
using Jade;
using Microsoft.Xna.Framework;

class Path : Entity{
    public Path(Game game, TileType[,] map, int i, int j) : base(game) {
        SetSprite("path", LayerType.Background);

        var right = new Point(i + 1, j);
        var left = new Point(i - 1, j);
        var bottom = new Point(i, j + 1);
        var top = new Point(i, j - 1);

        if (IsPath(top, map) && IsPath(left, map)) { // top left
            GetComponent<Sprite>().SetImageIndex(0);
        } else if (IsPath(top, map) && IsPath(right, map)) { // top right
            GetComponent<Sprite>().SetImageIndex(0);
        } else if (IsPath(bottom, map) && IsPath(left, map)) { // bottom left
            GetComponent<Sprite>().SetImageIndex(1);
        } else if (IsPath(bottom, map) && IsPath(right, map)) { // bottom right
            GetComponent<Sprite>().SetImageIndex(1);
        } else if (IsPath(top, map)) {
            GetComponent<Sprite>().SetImageIndex(0);
        } else if (IsPath(bottom, map)) {
            GetComponent<Sprite>().SetImageIndex(0);
        } else if (IsPath(left, map)) {
            GetComponent<Sprite>().SetImageIndex(1);
        } else if (IsPath(right, map)) {
            GetComponent<Sprite>().SetImageIndex(1);
        }
    }

    bool IsPath(Point other, TileType[,] map) {
        if (other.X >= 0 && other.X < map.GetLength(0) && other.Y >= 0 && other.Y < map.GetLength(1)) {
            return map[other.X, other.Y] == TileType.Path || map[other.X, other.Y] == TileType.castle;
        }

        return false;
    }
}