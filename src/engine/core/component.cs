using Microsoft.Xna.Framework;

namespace Jade;

abstract class Component {
    protected Entity parent { get; private set; }
    public Component(Entity _parent) {
        parent = _parent;
    }
    
    protected T GetComponent<T>() where T : Component { return parent.GetComponent<T>(); }
    protected void AddComponent<T>(T component) where T : Component { parent.AddComponent(component); }

    public void Dispose() {}
    public virtual void Update(GameTime gameTime) {}
    public virtual void Draw() {}
}