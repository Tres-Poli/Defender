using Entitas;

public sealed class PlayerSystems : Feature
{
    public PlayerSystems(Contexts contexts) : base("Player systems")
    {
        Add(new PlayerAttackSystem(contexts));
    }
}
