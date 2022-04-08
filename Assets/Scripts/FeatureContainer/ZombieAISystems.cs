using Entitas;
using System.Collections.Generic;

public sealed class ZombieAISystems : Feature
{
    public ZombieAISystems(Contexts contexts) : base("Zombie AI systems")
    {
        Add(new ZombieAISystem(contexts));
    }
}
