using Entitas;

public sealed class ViewSystems : Feature
{
    public ViewSystems(Contexts contexts) : base("View systems")
    {
        Add(new AddViewSystem(contexts));
    }
}
