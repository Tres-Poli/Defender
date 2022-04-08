using Entitas;
using System.Collections.Generic;
using UnityEngine;

public sealed class AnimationIdleSystem : ReactiveSystem<GameEntity>
{
    public AnimationIdleSystem(Contexts contexts) : base(contexts.game)
    {

    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Idling);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasView && entity.isPlayer && !entity.isMoving && !entity.isAttacking;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            var animator = e.view.Value.GetComponent<Animator>();
            animator.SetBool("Idle", true);
            animator.SetBool("Attack", false);
            animator.SetBool("Move", false);
        }
    }
}
