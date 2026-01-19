using System;

namespace Jade;

class Generator {
    Random Rand;

    public Generator(int seed) {
        Rand = new Random(seed);
    }

    public Generator() {
        Rand = new Random();
    }

    public void SetSeed(int seed) {
        Rand = new Random(seed);
    }

    public double NextDouble() {
        return Rand.NextDouble();
    }

    public double NextDouble(double min, double max) {
        return min + (max - min) * NextDouble();
    }

    public int Next() {
        return Rand.Next();
    }

    public int Next(int max) {
        return Rand.Next(max);
    }

    public int Next(int min, int max) {
        return Rand.Next(min, max);
    }
}