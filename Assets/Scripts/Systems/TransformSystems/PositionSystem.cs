using Entitas;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PositionSystem : ReactiveSystem<GameEntity>
{
    public PositionSystem(Contexts contexts) : base(contexts.game)
    {

    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Position);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasView;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            var go = e.view.Value;
            go.transform.position = e.position.Value;
        }
    }
}
    
