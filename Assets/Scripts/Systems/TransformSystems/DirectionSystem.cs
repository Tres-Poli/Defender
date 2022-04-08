using Entitas;
using System.Collections.Generic;
using UnityEngine;

public sealed class DirectionSystem : ReactiveSystem<GameEntity>
{
    public DirectionSystem(Contexts contexts) : base(contexts.game)
    {

    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.Direction));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasView;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            e.view.Value.transform.rotation = Quaternion.AngleAxis(e.direction.Value, Vector3.up);
        }
    }
}
