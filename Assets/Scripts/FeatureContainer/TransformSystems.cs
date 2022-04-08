using Entitas;

public sealed class TransformSystems : Feature
{
    public TransformSystems(Contexts contexts) : base("Transform systems")
    {
        Add(new MoveSystem(contexts));
        Add(new PositionSystem(contexts));
        Add(new DirectionSystem(contexts));
    }
}
