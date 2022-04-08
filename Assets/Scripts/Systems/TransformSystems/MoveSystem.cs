using Entitas;
using System.Collections.Generic;
using UnityEngine;

public sealed class MoveSystem : IExecuteSystem
{
    private IGroup<GameEntity> _moveGroup;

    public MoveSystem(Contexts contexts)
    {
        _moveGroup = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Move, GameMatcher.Position, GameMatcher.Direction, GameMatcher.Speed));
    }

    public void Execute()
    {
        foreach (var e in _moveGroup.GetEntities())
        {
            var moveDir = e.move.Value.normalized;
            var newPosition = e.position.Value + moveDir * e.speed.Value * Time.deltaTime;
            e.ReplacePosition(newPosition);

            if (moveDir != Vector3.zero)
            {
                var direction = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg;
                var lerpDirection = MathHelper.LerpForAtan2(e.direction.Value, direction, Time.deltaTime * 5);
                e.ReplaceDirection(lerpDirection);
            }
        }
    }
}
