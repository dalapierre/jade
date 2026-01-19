using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Jade;

class Mixer {
    Dictionary<MixerLayerType, MixerLayer> layers;
    public Mixer(Game game) {
        layers = new Dictionary<MixerLayerType, MixerLayer>
        {
            { MixerLayerType.Background, new MixerLayer(game, 0.30f) },
            { MixerLayerType.Game, new MixerLayer(game) },
        };
    }
    public void Play(MixerLayerType type, string sound) {
        var layer = layers[type];
        if (layer is not null) {
            layer.Play(sound);
        }
    }

    public void PlaySong(string song) {
        layers[MixerLayerType.Background].PlaySong(song);
    }

    public void Stop() {
        layers[MixerLayerType.Background].Stop();
    }

    public void SetVolume(MixerLayerType type, float level) {
        var layer = layers[type];
        if (layer is not null) {
            layer.SetVolume(level);
        }
    }
}