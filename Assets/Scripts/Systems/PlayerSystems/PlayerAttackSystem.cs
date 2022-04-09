using Entitas;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerAttackSystem : IExecuteSystem
{
    private IGroup<GameEntity> _player;

    private float _prevAttackTime;

    public PlayerAttackSystem(Contexts contexts)
    {
        _player = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Player, GameMatcher.Position, GameMatcher.Damage, GameMatcher.AttackRate, GameMatcher.Direction, GameMatcher.Target));
    }

    public void Execute()
    {
        foreach (var e in _player.GetEntities())
        {
            if (!e.isMoving)
            {
                if (Time.time - e.attackRate.Value >= _prevAttackTime)
                {
                    var target = e.target.Value;
                    if (target != null)
                    {
                        var attackDirection = target.position.Value - e.position.Value;
                        var direction = Mathf.Atan2(attackDirection.x, attackDirection.z) * Mathf.Rad2Deg;
                        e.ReplaceDirection(direction);

                        var obs = e.animationObserver.Value;
                        obs.SetOnAttackCast(target, () => target.ReplaceHealth(target.health.Value - e.damage.Value), e.position.Value);

                        _prevAttackTime = Time.time;

                        e.isAttacking = true;
                        e.isIdling = false;
                    }
                    else
                    {
                        e.isAttacking = false;
                        e.isIdling = true;
                    }
                }
            }
        }
    }
}
