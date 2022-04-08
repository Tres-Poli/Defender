using Entitas;
using System.Collections.Generic;
using UnityEngine;

public sealed class AnimationAttackSystem : ReactiveSystem<GameEntity>
{
    public AnimationAttackSystem(Contexts contexts) : base(contexts.game)
    {

    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Attacking);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isPlayer && entity.hasView && !entity.isMoving;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            var animator = e.view.Value.GetComponent<Animator>();
            animator.SetBool("Attack", true);
            animator.SetBool("Move", false);
            animator.SetBool("Idle", false);
        }
    }
}
