using Entitas;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerAttackSystem : IExecuteSystem
{
    private IGroup<GameEntity> _player;
    private IGroup<GameEntity> _zombies;

    private float _prevAttackTime;

    private List<GameEntity> _inRangeHostiles;

    public PlayerAttackSystem(Contexts contexts)
    {
        _player = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Player, GameMatcher.Position, GameMatcher.Damage, GameMatcher.AttackRate, GameMatcher.AttackRange));
        _zombies = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Zombie, GameMatcher.Position, GameMatcher.Health, GameMatcher.View));
        _inRangeHostiles = new List<GameEntity>(50);
    }

    public void Execute()
    {
        _inRangeHostiles.Clear();

        foreach (var e in _player.GetEntities())
        {
            if (!e.isMoving && Time.time - e.attackRate.Value >= _prevAttackTime)
            {
                var hostiles = _zombies.GetEntities();
                GameEntity closestInRangeHostile = null;
                float closestInRangeDistance = 0;
                int lastCheckedHostile = 0;
                for (int i = 0; i < hostiles.Length; i++)
                {
                    lastCheckedHostile = i;
                    if (MathHelper.IsInAttackRange(hostiles[i].position.Value, e.position.Value, e.attackRange.Value, out closestInRangeDistance))
                    {
                        closestInRangeHostile = hostiles[i];
                        break;
                    }
                }

                for (int i = lastCheckedHostile; i < hostiles.Length; i++)
                {
                    float currDistance;
                    if (MathHelper.IsInAttackRange(hostiles[i].position.Value, e.position.Value, e.attackRange.Value, out currDistance))
                    {
                        if (currDistance < closestInRangeDistance)
                        {
                            closestInRangeHostile = hostiles[i];
                            closestInRangeDistance = currDistance;
                        }
                    }
                }

                if (closestInRangeHostile != null)
                {
                    e.isAttacking = true;
                    closestInRangeHostile.ReplaceHealth(closestInRangeHostile.health.Value - e.damage.Value);
                    Debug.Log($"Attacking zombie {closestInRangeHostile.view.Value.name}");
                    _prevAttackTime = Time.time;
                }
                else
                {
                    e.isAttacking = false;
                }
            }
        }
    }
}
