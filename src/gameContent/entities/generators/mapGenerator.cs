using System.Collections.Generic;
using Jade;
using Microsoft.Xna.Framework;

class MapGenerator : Entity {
    const double ROCK_ODDS = 0.05f;
    const int CELL_DIM = 16;
    Generator gen;
    SceneManager sm;
    int width;
    int height;

    public MapGenerator(Game game, int _width, int _height) : base(game) {
        gen = game.Services.GetService<Generator>();
        sm = game.Services.GetService<SceneManager>();
        width = _width;
        height = _height;
    }

    public override void Update(GameTime gameTime) {
        base.Update(gameTime);
    }

    public (TileType[,], List<Vector2>) GenerateMap() {
        TileType[,] map;
        (map, var path, var hMap) = Generate(width, height, gen);

        ClearChildren();

        for (int i = 0; i < map.GetLength(0); i++) {
            for (int j = 0; j < map.GetLength(1); j++) {
                var xx = sm.viewX + i * CELL_DIM;
                var yy = sm.viewY + j * CELL_DIM;
                
                switch(map[i,j]) {
                    case TileType.Floor:
                        AddChildren(new Floor(game), xx, yy);
                        break;
                    case TileType.Path:
                        AddChildren(new Path(game, map, i, j), xx, yy);
                        break;
                }
            }
        }

        return (map, path);
    }

    (TileType[,], List<Vector2>, float[,]) Generate(int w, int h, Generator gen = null) {
        if (gen is null) gen = new Generator();
        TileType[,] map = new TileType[w, h];
        for (int i = 0; i < w; i++) {
            for (int j = 0; j < h; j++) {
                map[i, j] = TileType.Floor;
            }
        }

        (map, var path, var hMap) = GeneratePath(map, gen);

        return (map, path, hMap);
    }

    (TileType[,], List<Vector2>, float[,]) GeneratePath(TileType[,] map, Generator gen) {
        var w = map.GetLength(0);
        var h = map.GetLength(1);

        var hMap = GenerateHeightMap(w, h, gen);

        // Step 1: Find a starting point on the left side
        int startX = 0; // Always start on the left edge
        var minH = (int)(h * 0.15f);
        var maxH = (int)(h * 0.85f);
        int startY = gen.Next(minH, maxH);

        // Step 2: Find the target point on the right side
        int targetX = map.GetLength(0) - gen.Next(4, 8);
        int targetY = gen.Next(minH, maxH);;

        // Step 3: Generate the path
        Dictionary<Vector2, Vector2> origins = new Dictionary<Vector2, Vector2>();
        var unvisited = new List<Vector2>();
        var visited = new List<Vector2>();

        var dist = new float[w, h];
        for (int i = 0; i < w; i++) {
            for (int j = 0; j < h; j++) {
                dist[i, j] = int.MaxValue;
                unvisited.Add(new Vector2(i, j));
            }
        }

        dist[startX, startY] = 0;
        var next = new Vector2(startX, startY);

        var queue = new PriorityQueue<Vector2, float>();
        queue.Enqueue(next, 0);

        while (queue.Count > 0) {
            var current = queue.Dequeue();
            if (unvisited.Contains(current)) {
                visited.Add(current);
                var neighbors = new List<Vector2> {
                    new Vector2(current.X + 1, current.Y),
                    new Vector2(current.X - 1, current.Y),
                    new Vector2(current.X, current.Y + 1),
                    new Vector2(current.X, current.Y - 1)
                };

                foreach (var neighbor in neighbors) {
                    if (unvisited.Contains(neighbor)) {
                        float weight = dist[(int)current.X, (int)current.Y] + hMap[(int)neighbor.X, (int)neighbor.Y];
                        if (weight < dist[(int)neighbor.X, (int)neighbor.Y]) {
                            dist[(int)neighbor.X, (int)neighbor.Y] = weight;
                            if (origins.ContainsKey(neighbor)) {
                                origins[neighbor] = current;
                            } else {
                                origins.Add(neighbor, current);
                            }
                            queue.Enqueue(neighbor, weight);
                        }
                    }
                }
            }
        }

        var lastNode = new Vector2(targetX, targetY);

        List<Vector2> path = new List<Vector2>();

        while (lastNode != new Vector2(startX, startY)) {
            path.Add(new Vector2(lastNode.X * CELL_DIM, lastNode.Y * CELL_DIM));
            map[(int)lastNode.X, (int)lastNode.Y] = TileType.Path;
            lastNode = origins[lastNode];
        }

        path.Add(new Vector2(lastNode.X * CELL_DIM, lastNode.Y * CELL_DIM));
        map[(int)lastNode.X, (int)lastNode.Y] = TileType.Path;

        path.Reverse();

        return (map, path, hMap);
    }

    public float[,] GenerateHeightMap(int w, int h, Generator gen) {
        var pass = new Pass();
        var centerX = gen.Next(-1000, 1000);
        var centerY = gen.Next(-1000, 1000);
        pass.AddAlgo(new Algo(gen, NoiseType.Simplex, 0.08f, new Vector2(centerX, centerY), 0));
        pass.AddAlgo(new Algo(gen, NoiseType.Square, 1, Vector2.Zero, 0, 25));

        var hMap = pass.Apply(w, h, 1.4f);

        for (int i = 0; i < w; i++) {
            for (int j = 0; j < h; j++) {
                var original = hMap[i, j];
                hMap[i, j] = (original + 1) / 2;
            }
        }

        return hMap;
    }

    public float[,] GenerateMenuBackground(int w, int h, Generator gen) {
        var pass = new Pass();
        var centerX = gen.Next(-1000, 1000);
        var centerY = gen.Next(-1000, 1000);
        pass.AddAlgo(new Algo(gen, NoiseType.Simplex, 0.08f, new Vector2(centerX, centerY), 0));

        var hMap = pass.Apply(w, h, 1.4f);

        for (int i = 0; i < w; i++) {
            for (int j = 0; j < h; j++) {
                var original = hMap[i, j];
                hMap[i, j] = (original + 1) / 2;
            }
        }

        return hMap;
    }
}