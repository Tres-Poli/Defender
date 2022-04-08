using Entitas;

public sealed class InitSystems : Feature
{
    public InitSystems(Contexts contexts) : base("Init systems")
    {
        Add(new InitializePlayerSystem(contexts));
    }
}
