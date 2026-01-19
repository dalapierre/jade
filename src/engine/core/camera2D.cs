using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Jade;

class Camera2D : Camera {
    GraphicsDevice device;
    public int viewW { get; private set; }
    public int viewH { get; private set; }
    Entity target;
    Vector2 offset;

    public int viewX {
        get {
            var tarPos = target is not null ? target.Position : Vector3.Zero;
            return (int)(-tarPos.X - position.X - offset.X);
        }
    }

    public int viewY {
        get {
            var tarPos = target is not null ? target.Position : Vector3.Zero;
            return (int)(-tarPos.Y - position.Y - offset.Y);
        }
    }

    public override Matrix worldMat {
        get {
            var tarPos = target is not null ? target.Position : Vector3.Zero;
            var x = -tarPos.X - position.X - offset.X;
            var y = -tarPos.Y - position.Y - offset.Y;
            return Matrix.CreateTranslation(new Vector3(x, y, 0)) *
                   Matrix.CreateScale(new Vector3(scale.X, scale.Y, 1));// *
                   //Matrix.CreateTranslation(new Vector3(device.Viewport.Width / 2.0f, device.Viewport.Height / 2.0f, 0));
        }
    }

    public Camera2D(Game game, int _viewW, int _viewH) : base(int.MaxValue) {
        device = game.GraphicsDevice;
        viewW = _viewW;
        viewH = _viewH;
        var xScale = device.Viewport.Width / viewW;
        var yScale = device.Viewport.Height / viewH;
        SetScale(xScale, yScale);
    }

    public void SetTarget(Entity _target) {
        target = _target;
    }

    public void SetOffset(int x, int y) {
        offset = new Vector2(x,  y);
    }
}