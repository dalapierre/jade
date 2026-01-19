using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Jade;

class SceneManager {
    Scene currentScene;
    Dictionary<Type, Scene> scenes;

    public int viewX {
        get {
            var cam = GetCamera<Camera2D>();
            return cam is not null ? cam.viewX : 0;
        }
    }

    public int viewY {
        get {
            var cam = GetCamera<Camera2D>();
            return cam is not null ? cam.viewY : 0;
        }
    }

    public SceneManager() {
        scenes = new Dictionary<Type, Scene>();
    }

    public void CreateEntity(Entity entity, int x, int y) {
        if (currentScene is not null) currentScene.CreateEntity(entity, x, y);
    }

    public void RemoveEntity(Guid entity) {
        if (currentScene is not null) currentScene.RemoveEntity(entity);
    }

    public T GetCamera<T>() where T : Camera {
        return currentScene is not null ? currentScene.GetCamera<T>() : null;
    }

    public List<Entity> GetEntities() { return currentScene.GetEntities(); }
    public List<T> GetEntities<T, W>() where T : Entity where W : Component { return currentScene.GetEntities<T, W>(); }
    public List<T> GetEntities<T>() where T : Entity { return currentScene.GetEntities<T>(); }

    public void Update(GameTime gameTime) {
        if (currentScene is not null) currentScene.Update(gameTime);
    }

    public void Draw() {
        if (currentScene is not null) currentScene.Draw();
    }

    public void SetScene<T>() where T : Scene {
        var key = typeof(T);
        if (scenes.ContainsKey(key)) {
            if (currentScene is not null) currentScene.Dispose();
            currentScene = GetScene<T>();
            currentScene.Init();
        }
    }

    public void AddScece<T>(T scene) where T : Scene {
        var key = typeof(T);
        if (!scenes.ContainsKey(key)) {
            scenes.Add(key, scene);
            if (currentScene is null) SetScene<T>();
        }
    }

    public T GetScene<T>() where T : Scene {
        var key = typeof(T);
        return scenes.ContainsKey(key) ? scenes[key] as T : null;
    }
}