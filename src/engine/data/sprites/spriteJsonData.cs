using System.Collections.Generic;

public class SpriteContent {
    public string name { get; set; }
    public double animationSpeed { get; set; }
    public string texturePath { get; set; }
    public OriginData origin { get; set; }
    public List<FrameData> frames { get; set; }
}

public class OriginData {
    public int x { get; set; }
    public int y { get; set; }
}

public class FrameData {
    public int x { get; set; }
    public int y { get; set; }
    public int w { get; set; }
    public int h { get; set; }
}

public class SpriteJsonData {
    public List<SpriteContent> sprites { get; set; }
}
