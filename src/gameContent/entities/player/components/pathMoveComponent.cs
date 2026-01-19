using System;
using System.Collections.Generic;
using Jade;
using Microsoft.Xna.Framework;

class PathMoveComponent : Component {
    const float THRESHOLD = 1f;
    public float speed { get; private set; }
    List<Vector2> path;
    int currentPathTarget;

    public PathMoveComponent(Entity _parent, float _speed) : base(_parent) {
        currentPathTarget = 1;
        speed = _speed;
    }

    public void SetPath(List<Vector2> _path) { path = _path; }

    public override void Update(GameTime gameTime) {
        if (path is not null && currentPathTarget < path.Count) {
            if (currentPathTarget < path.Count) {
                var pos = new Vector2(parent.Position.X, parent.Position.Y);
                var target = path[currentPathTarget];

                var direction = target - pos;
                direction.Normalize();
                var spd = speed * gameTime.ElapsedGameTime.TotalSeconds;

                var xx = pos.X + direction.X * spd;
                var yy = pos.Y + direction.Y * spd;

                if (xx <= target.X + THRESHOLD && xx >= target.X - THRESHOLD &&
                yy <= target.Y + THRESHOLD && yy >= target.Y - THRESHOLD) {
                    parent.SetPosition(target.X, target.Y);
                    ++currentPathTarget;
                } else {
                    parent.Move((float)(direction.X * spd), (float)(direction.Y * spd));
                }
            }
        }
    }
}