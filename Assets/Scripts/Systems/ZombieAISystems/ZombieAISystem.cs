using Entitas;
using System.Collections.Generic;

public sealed class ZombieAISystem : IExecuteSystem
{
    private IGroup<GameEntity> _zombies;

    private float _attackDistance;

    public ZombieAISystem(Contexts contexts)
    {
        _zombies = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Zombie, GameMatcher.Move, GameMatcher.View));
    }

    public void Execute()
    {
        foreach (var e in _zombies.GetEntities())
        {
            var moveDir = GameController.Player.view.Value.transform.position - e.view.Value.transform.position;
            moveDir.y = 0f;
            e.ReplaceMove(moveDir);
        }
    }
}
