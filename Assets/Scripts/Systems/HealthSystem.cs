using Entitas;
using Entitas.Unity;
using System.Collections.Generic;
using UnityEngine;

public sealed class HealthSystem : ReactiveSystem<GameEntity>
{
    public HealthSystem(Contexts contexts) : base(contexts.game)
    {

    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Health);
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            if (e.health.Value <= 0)
            {
                if (e.hasView)
                {
                    e.view.Value.Unlink();
                    Object.Destroy(e.view.Value);
                }

                e.Destroy();
            }
        }
    }
}
