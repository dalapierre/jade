using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Jade;

abstract class Entity : TransformElement {
    protected Game game { get; private set; }
    protected Dictionary<Guid, Entity> children;
    public Guid ID { get; private set; }
    Dictionary<Type, Component> components { get; set; }
    SpriteLoader spriteLoader;
    SceneManager sm;

    public Entity(Game _game) : base() {
        game = _game;
        sm = game.Services.GetService<SceneManager>();
        children = new Dictionary<Guid, Entity>();
        ID = Guid.NewGuid();
        spriteLoader = game.Services.GetService<SpriteLoader>();
        components = new Dictionary<Type, Component>();
        AddComponent(new Sprite(game, this));
        AddComponent(new Collider(game, this));
    }

    protected void AddChildren(Entity entity, int x, int y) {
        if (!children.ContainsKey(entity.ID)) {
            children.Add(entity.ID, entity);
        }

        sm.CreateEntity(entity, x, y);
    }

    protected void RemoveChild(Entity entity) {
        if (children.ContainsKey(entity.ID)) {
            children.Remove(entity.ID);
        }

        sm.RemoveEntity(entity.ID);
    }

    public void ClearChildren() {
        foreach (var child in children.Values) {
            child.Dispose();
        }

        children.Clear();
    }

    protected void SetSprite(string spriteName, LayerType layer = LayerType.Game) {
        var data = spriteLoader.Get(spriteName);
        if (data is not null) GetComponent<Sprite>().SetSprite(data, layer);
    }

    public virtual void Update(GameTime gameTime) {
        foreach (var component in components.Values) {
            component.Update(gameTime);
        }
    }

    public virtual void Draw() {
        foreach (var component in components.Values) {
            component.Draw();
        }
    }

    public void AddComponent<T>(T component) where T : Component {
        var key = typeof(T);
        if (!components.ContainsKey(key)) components.Add(key, component);
    }

    public T GetComponent<T>() where T : Component {
        var key = typeof(T);
        return components.ContainsKey(key) ? components[key] as T : null;
    }

    public bool HasComponent<T>() where T : Component {
        return components.ContainsKey(typeof(T));
    }

    public void DisposeComponent<T>() where T : Component {
        var key = typeof(T);
        if (components.ContainsKey(key)) {
            components[key].Dispose();
            components.Remove(key);
        }
    }

    public void Dispose() {
        foreach (var child in children.Values) {
            child.Dispose();
        }

        children.Clear();
        sm.RemoveEntity(ID);
    }
}