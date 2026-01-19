using Jade;
using Microsoft.Xna.Framework;

class BackgroundTile : Entity {
    const double SPEED = 2f;
    const double ANIM_SPEED = 0.1;
    double elapsedTime;
    bool ascending;
    double height;
    bool isEnding;

    public BackgroundTile(Game _game, float _height) : base(_game) {
        ascending = true;
        elapsedTime = 0;
        height = _height;
        SetSprite("floor", LayerType.Background);
        var val = (int)(height * 255);
        GetComponent<Sprite>().SetColor(new Color(val, val, val, val));
    }

    public void SetEnding() {
        isEnding = true;
    }

    public override void Update(GameTime gameTime) {
        if (!isEnding) {
            elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;
            if (elapsedTime >= ANIM_SPEED) {
                elapsedTime = 0;
                if (ascending) {
                    height += SPEED * gameTime.ElapsedGameTime.TotalSeconds;
                    if (height >= 1) {
                        height = 1;
                        ascending = false;
                    }
                } else {
                    height -= SPEED * gameTime.ElapsedGameTime.TotalSeconds;
                    if (height <= 0) {
                        height = 0;
                        ascending = true;
                    }
                }
            }
        } else {
            height -= SPEED * gameTime.ElapsedGameTime.TotalSeconds;
            if (height <= 0) {
                height = 0;
            }
        }
        var val = (int)(height * 255);
        GetComponent<Sprite>().SetColor(new Color(val, val, val, val));
        base.Update(gameTime);
    }
}