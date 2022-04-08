using Entitas;
using System.Collections.Generic;
using UnityEngine;

public sealed class AnimationSystem : ReactiveSystem<GameEntity>
{
    public AnimationSystem(Contexts contexts) : base(contexts.game)
    {

    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AnyOf(GameMatcher.View, GameMatcher.Moving, GameMatcher.Attacking, GameMatcher.Player));
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            var animator = e.view.Value.GetComponent<Animator>();
            if (e.isMoving)
            {
                animator.SetTrigger("Move");
            }
            else if (e.isAttacking)
            {
                animator.SetTrigger("Attack");
            }
            else
            {
                animator.SetTrigger("Idle");
            }
        }
    }
}
