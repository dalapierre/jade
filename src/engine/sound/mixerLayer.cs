using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Jade;

class MixerLayer {
    Game game;
    float volume;
    Dictionary<string, SoundEffect> sounds;
    float volumeOffset;

    public MixerLayer(Game _game, float _volumeOffset = 1) {
        volumeOffset = _volumeOffset;
        game = _game;
        SetVolume(1);
        sounds = new Dictionary<string, SoundEffect>();
    }

    SoundEffect Load(string sound) {
        if (!sounds.ContainsKey(sound)) {
            var effect = game.Content.Load<SoundEffect>("audio/"+sound);
            sounds.Add(sound, effect);
        }

        return sounds[sound];
    }

    public void Stop() {
        MediaPlayer.Stop();
    }
    
    public void PlaySong(string sound) {
        var s = game.Content.Load<Song>($"audio/{sound}");
        MediaPlayer.Play(s);
        MediaPlayer.Volume = volume;
        MediaPlayer.IsRepeating = true;
    }

    public void Play(string sound) {
        var effect = Load(sound);
        var instance = effect.CreateInstance();
        instance.Volume = volume;
        instance.Play();
    }

    public void SetVolume(float level) {
        if (level < 0) level = 0;
        if (level > 1) level = 1;
        volume = level * volumeOffset;
    }

}