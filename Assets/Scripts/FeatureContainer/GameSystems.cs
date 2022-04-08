using Entitas;

public sealed class GameSystems : Feature
{
    public GameSystems(Contexts contexts) : base("Game systems")
    {
        Add(new SpawnZombieSystem(contexts));
        Add(new HealthSystem(contexts));
    }
}
