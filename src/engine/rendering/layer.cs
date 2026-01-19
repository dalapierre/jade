using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Jade;

class Layer {
    SpriteBatch spriteBatch;
    List<Action> drawQueue;

    public Layer(SpriteBatch _spriteBatch) {
        drawQueue = new List<Action>();
        spriteBatch = _spriteBatch;
    }

    public void Draw(SpriteSortMode sortMode = SpriteSortMode.BackToFront, BlendState blendState = null, SamplerState samplerState = null, DepthStencilState depthStencilState = null, RasterizerState rasterizerState = null, Effect effect = null, Matrix? transformMatrix = null) {
        spriteBatch.Begin(sortMode, blendState, samplerState, depthStencilState, rasterizerState, effect, transformMatrix);
        foreach (var call in drawQueue) {
            call.Invoke();
        }
        drawQueue.Clear();
        spriteBatch.End();
    }

    public void AddToQueue(Action drawCall) {
        drawQueue.Add(drawCall);
    }
}