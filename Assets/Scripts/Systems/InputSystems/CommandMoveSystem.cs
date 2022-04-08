using Entitas;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandMoveSystem : ReactiveSystem<InputEntity>
{
    private IGroup<GameEntity> _movables;

    public CommandMoveSystem(Contexts contexts) : base(contexts.input)
    {
        _movables = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Move, GameMatcher.Player));
    }

    protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
    {
        return context.CreateCollector(InputMatcher.MoveInput);
    }

    protected override bool Filter(InputEntity entity)
    {
        return true;
    }

    protected override void Execute(List<InputEntity> entities)
    {
        foreach (var e in entities)
        {
            foreach (var movable in _movables.GetEntities())
            {
                movable.ReplaceMove(e.moveInput.Value);
            }
        }
    }
}
