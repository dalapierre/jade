using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Jade;

abstract class Scene {
    Dictionary<Guid, Entity> entities;
    Renderer renderer;
    protected Game game;
    protected Camera camera;
    List<Entity> entitiesToAdd;
    List<Guid> entitiesToRemove;

    public Scene(Game _game) {
        game = _game;
        entitiesToAdd = new List<Entity>();
        entitiesToRemove = new List<Guid>();
        renderer = game.Services.GetService<Renderer>();
        entities = new Dictionary<Guid, Entity>();
    }

    public abstract void Init();

    public List<Entity> GetEntities() {
        return entities.Values.ToList();
    }

    public List<T> GetEntities<T>() where T : Entity {
        return entities.Values.OfType<T>().ToList();
    }

    public List<T> GetEntities<T, W>() where T : Entity where W : Component {
        return entities.Values.OfType<T>().Where(x => x.HasComponent<W>()).ToList();
    }

    public void SetCamera(Camera _camera) {
        camera = _camera;
    }

    public T GetCamera<T>() where T : Camera {
        return camera as T;
    }
    
    public void CreateEntity(Entity entity, int x, int y) {
        if (!entities.ContainsKey(entity.ID)) {
            entity.SetPosition(x, y);
            entitiesToAdd.Add(entity);
        }
    }

    public void RemoveEntity(Guid id) {
        if (entities.ContainsKey(id)) {
            entitiesToRemove.Add(id);
        }
    }

    void AddQueuedEntities() {
        foreach (var entity in entitiesToAdd) {
            entities.Add(entity.ID, entity);
        }
        entitiesToAdd.Clear();
    }

    void RemoveQueuedEntities() {
        foreach (var entity in entitiesToRemove) {
            entities.Remove(entity);
        }
        entitiesToRemove.Clear();
    }

    public void DeleteEntity(Guid id) {
        if (entities.ContainsKey(id)) {
            entities[id].Dispose();
            entities.Remove(id);
        }
    }

    public void Update(GameTime gameTime) {
        RemoveQueuedEntities();
        AddQueuedEntities();
        if (camera is not null) camera.Update(gameTime);
        foreach (var entity in entities.Values) {
            entity.Update(gameTime);
        }
    }

    public void Draw() {
        foreach (var entity in entities.Values) {
            entity.Draw();
        }
        renderer.Draw(camera);
    }

    public void Dispose() {
        entities.Clear();
    }
}