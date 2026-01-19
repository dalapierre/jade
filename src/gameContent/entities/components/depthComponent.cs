using Jade;
using Microsoft.Xna.Framework;

class DepthComponent : Component {
    int additive;
    public DepthComponent(Entity _parent, int _additive = 0) : base(_parent) {
        additive = _additive;
    }

    public override void Update(GameTime gameTime) {
        var depth = -parent.Position.Y - additive;
        parent.SetDepth(depth);
    }
}