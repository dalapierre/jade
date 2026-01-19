using Jade;

class MessageBoxIconData {
    public SpriteData data { get; private set; }
    public string text { get; private set; }
    public int index { get; private set; }

    public MessageBoxIconData(SpriteData _data, int _index, string _text) {
        data = _data;
        text = _text;
        index = _index;
    }
}