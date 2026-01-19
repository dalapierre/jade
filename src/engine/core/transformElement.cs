using Microsoft.Xna.Framework;

namespace Jade;

class TransformElement {
    protected Vector3 position;
    protected Vector3 rotation;
    protected Vector2 scale;

    public Vector3 Position { get { return position; } }
    public Vector3 Rotation { get { return rotation; } }
    public Vector2 Scale { get { return scale; } }
    public float Roll { get { return rotation.Z; } }

    public TransformElement() {
        rotation = Vector3.Zero;
        position = Vector3.Zero;
        scale = Vector2.One;
    }


    public void SetScale(float _scale) {
        scale = new Vector2(_scale, _scale);
    }

    public void SetScale(float xScale, float yScale) {
        scale = new Vector2(xScale, yScale);
    }

    public void SetPosition(float x, float y) {
        position.X = x;
        position.Y = y;
    }

    public void SetPosition(float x, float y, float z) {
        position.X = x;
        position.Y = y;
        position.Z = z;
    }

    public void SetDepth(float depth) {
        position.Z = depth;
    }

    public void MoveDepth(int depth) {
        position.Z += depth;
    }

    public void Move(float x, float y, float z) {
        position.X += x;
        position.Y += y;
        position.Z += z;
    }

    public void Move(float x, float y) {
        position.X += x;
        position.Y += y;
    }

    public void SetRotation(float pitch, float yaw, float roll) {
        rotation.X = pitch;
        rotation.Y = yaw;
        rotation.Z = roll;
    }

    public void RotateZ(float roll) {
        rotation.Z += roll;
    }

    public void RotateY(float yaw) {
        rotation.Y += yaw;
    }

    public void RotateX(float pitch) {
        rotation.X += pitch;
    }

    public void Rotate(float pitch, float yaw, float roll) {
        rotation.X += pitch;
        rotation.Y += yaw;
        rotation.Z += roll;
    }

    public float DistanceTo(Vector3 other) {
        return Vector3.Distance(position, other);
    }

    public float DistanceTo(Vector2 other) {
        return Vector2.Distance(new Vector2(position.X, position.Y), other);
    }
}