using Microsoft.Xna.Framework;

namespace Jade;

abstract class Camera : TransformElement {
    protected float near;
    protected int viewDistance;

    public abstract Matrix worldMat { get; }

    public Camera(int _viewDistance) : base() {
        near = 0.1f;
        viewDistance = _viewDistance;
    }

    public virtual void Update(GameTime gameTime) {}
}