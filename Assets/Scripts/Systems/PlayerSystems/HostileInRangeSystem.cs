using Entitas;
using System.Collections.Generic;
using UnityEngine;

public sealed class HostileInRangeSystem : IExecuteSystem
{
    private IGroup<GameEntity> _playerChars;
    private IGroup<GameEntity> _zombieChars;

    public HostileInRangeSystem(Contexts contexts)
    {
        _playerChars = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Player, GameMatcher.AttackRate, GameMatcher.AttackRange, GameMatcher.Position, GameMatcher.Target));
        _zombieChars = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Zombie, GameMatcher.AttackRate, GameMatcher.AttackRange, GameMatcher.Position));
    }

    public void Execute()
    {
        foreach (var p in _playerChars)
        {
            var hostiles = _zombieChars.GetEntities();
            GameEntity closestInRangeHostile = null;
            float closestInRangeDistance = 0;
            int lastCheckedHostile = 0;
            for (int i = 0; i < hostiles.Length; i++)
            {
                lastCheckedHostile = i;
                if (MathHelper.IsInAttackRange(hostiles[i].position.Value, p.position.Value, p.attackRange.Value, out closestInRangeDistance))
                {
                    closestInRangeHostile = hostiles[i];
                    break;
                }
            }

            for (int i = lastCheckedHostile; i < hostiles.Length; i++)
            {
                float currDistance;
                if (MathHelper.IsInAttackRange(hostiles[i].position.Value, p.position.Value, p.attackRange.Value, out currDistance))
                {
                    if (currDistance < closestInRangeDistance)
                    {
                        closestInRangeHostile = hostiles[i];
                        closestInRangeDistance = currDistance;
                    }
                }
            }

            if (p.target.Value != closestInRangeHostile)
            {
                p.ReplaceTarget(closestInRangeHostile);
            }
        }
    }
}
