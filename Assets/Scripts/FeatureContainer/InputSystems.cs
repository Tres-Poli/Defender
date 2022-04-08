using Entitas;

public sealed class InputSystems : Feature
{
    public InputSystems(Contexts contexts) : base("Input systems")
    {
        Add(new InputEmitSystem(contexts));
        Add(new CommandMoveSystem(contexts));
    }
}
