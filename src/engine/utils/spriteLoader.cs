using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Jade;

class SpriteLoader {
    Game game;
    Dictionary<string, Texture2D> textures;
    Dictionary<string, SpriteData> data;

    public SpriteLoader(Game _game) {
        game = _game;
        textures = new Dictionary<string, Texture2D>();
        data = new Dictionary<string, SpriteData>();
    }

    public void Initialize(string spriteDataPath) {
        string json;

        using (Stream stream = TitleContainer.OpenStream("Content/data/"+spriteDataPath)) {
            using (StreamReader reader = new StreamReader(stream)) {
                json = reader.ReadToEnd();
            }
        }

        var rawDatas = JsonSerializer.Deserialize<SpriteJsonData>(json);
        
        foreach (var rawData in rawDatas.sprites) {
            var texture = LoadTexture(rawData.texturePath);
            Rectangle[] frames = new Rectangle[rawData.frames.Count];

            for (int i = 0; i < rawData.frames.Count; i++) {
                var frameData = rawData.frames[i];
                frames[i] = new Rectangle(frameData.x, frameData.y, frameData.w, frameData.h);
            }

            var origin = rawData.origin is not null ? new Vector2(rawData.origin.x, rawData.origin.y) : Vector2.Zero;
            var usableData = new SpriteData(rawData.name, texture, rawData.animationSpeed, frames, origin);
            data.Add(rawData.name, usableData);
        }
    }

    Texture2D LoadTexture(string path) {
        if (!textures.ContainsKey(path)) {
            textures.Add(path, game.Content.Load<Texture2D>(path));
        }

        return textures[path];
    }

    public SpriteData Get(string name) {
        return data.ContainsKey(name) ? data[name] : null;
    }
}