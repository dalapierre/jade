using Jade;

class ResourceComponent : Component {
    public int resource { get; private set; }
    public ResourceComponent(Entity _parent) : base(_parent) {
        resource = 0;
    }

    public void AddResource(int value) { resource += value; }
    public void SetRessource(int value) { resource = value; }
    public bool SpendResource(int value) {
        if (resource >= value) {
            resource -= value;
            return true;
        }

        return false;
    }
}