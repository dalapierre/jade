using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Jade;

class Renderer {
    SpriteBatch spriteBatch;
    Dictionary<LayerType, Layer> layers;

    public Renderer(SpriteBatch _spriteBatch) {
        layers = new Dictionary<LayerType, Layer>
        {
            { LayerType.Background, new Layer(_spriteBatch) },
            { LayerType.Game, new Layer(_spriteBatch) },
            { LayerType.UI, new Layer(_spriteBatch) }
        };
        spriteBatch = _spriteBatch;
    }

    public void Draw(Camera camera) {
        var transformMat = camera is not null ? camera.worldMat : Matrix.Identity;
        layers[LayerType.Background].Draw(samplerState: SamplerState.PointWrap, blendState: BlendState.AlphaBlend, transformMatrix: transformMat);
        layers[LayerType.Game].Draw(samplerState: SamplerState.PointWrap, blendState: BlendState.AlphaBlend, transformMatrix: transformMat);
        layers[LayerType.UI].Draw(samplerState: SamplerState.PointWrap, blendState: BlendState.AlphaBlend);
    }

    public void DrawTo(LayerType layer, Texture2D texture, Vector2 position, Rectangle sourceRectangle, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effect, float depth) {
        Action drawAction = new Action(() => {
            var layerDepth = (float)(depth + Math.Pow(2, 20)) / (float)(2 * Math.Pow(2, 20));
            spriteBatch.Draw(texture, position, sourceRectangle, color, rotation, origin, scale, effect, layerDepth);
        });
        layers[layer].AddToQueue(drawAction);
    }

    public void DrawStringTo(LayerType layer, SpriteFont font, string text, Vector2 position, Color color, float rotation, Vector2 origin, int size, SpriteEffects effect, float depth) {
        Action drawStringAction = new Action(() => {
            var scale = size / 12.0f;
            spriteBatch.DrawString(font, text, position, color, rotation, origin, scale, effect, depth);
        });
        layers[layer].AddToQueue(drawStringAction);
    }
}