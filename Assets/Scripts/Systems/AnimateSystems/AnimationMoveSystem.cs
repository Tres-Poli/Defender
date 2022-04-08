using Entitas;
using System.Collections.Generic;
using UnityEngine;

public sealed class AnimationMoveSystem : ReactiveSystem<GameEntity>
{
    private IGroup<GameEntity> _animated;

    public AnimationMoveSystem(Contexts contexts) : base(contexts.game)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Moving);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isPlayer && entity.hasView;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            var animator = e.view.Value.GetComponent<Animator>();
            animator.SetBool("Move", true);
            animator.SetBool("Attack", false);
            animator.SetBool("Idle", false);
            Debug.Log("Triggered move");
        }
    }
}
