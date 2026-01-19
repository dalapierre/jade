namespace Jade;
class HeightCell {
    public string Name { get; private set; }
    public float Cutoff { get; private set; }
    public bool OnLand { get; private set; }
    public HeightCell(string name, float cutoff, bool onLand = false) {
        Name = name;
        Cutoff = cutoff;
        OnLand = onLand;
    }
}