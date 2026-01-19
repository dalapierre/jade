using System;
using Jade;
using Microsoft.Xna.Framework;

class Collider : Component {
    SceneManager sm;
    public Rectangle bounds { get; private set; }
    int width;
    int height;

    public Collider(Game game, Entity _parent) : base(_parent) {
        sm = game.Services.GetService<SceneManager>();
    }

    public override void Update(GameTime gameTime) {
        if (width > 0 && height > 0) {
            bounds = new Rectangle((int)parent.Position.X, (int)parent.Position.Y, width, height);
        }
    }

    public void SetBounds(int w, int h) {
        width = w;
        height = h;
        bounds = new Rectangle((int)parent.Position.X, (int)parent.Position.Y, w, h);
    }

    public bool CheckCollision(Entity entity) {
        var collider = entity.GetComponent<Collider>();
        return bounds.Intersects(collider.bounds);
    }

    public T CheckCollision<T>() where T : Entity {
        var pos = new Point((int)parent.Position.X, (int)parent.Position.Y);
        bounds = new Rectangle(pos.X, pos.Y, width, height);

        var collisionEntities = sm.GetEntities<T, Collider>();

        foreach (var entity in collisionEntities) {
            if (entity.ID != parent.ID) {
                var collider = entity.GetComponent<Collider>();
                if (bounds.Intersects(collider.bounds)) {
                    return entity;
                }
            }
        }

        return null;
    }
}