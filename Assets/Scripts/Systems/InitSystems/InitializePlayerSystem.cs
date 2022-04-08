using Entitas;
using System.Linq;
using UnityEngine;

public sealed class InitializePlayerSystem : IInitializeSystem
{
    private Context<GameEntity> _context;
    private CharacterConfig _playerConfig;

    public InitializePlayerSystem(Contexts contexts)
    {
        _context = contexts.game;
        _playerConfig = GameSettings.CharStats.FirstOrDefault(x => x.Type == CharacterType.Player);
        if (_playerConfig == null)
        {
            throw new System.Exception("Player config is missing");
        }
    }

    public void Initialize()
    {
        var e = _context.CreateEntity();
        e.isPlayer = true;
        e.AddPrefab("Player");
        e.AddHealth(_playerConfig.Health);
        e.AddDamage(_playerConfig.Damage);
        e.AddAttackRate(_playerConfig.AttackRate);
        e.AddAttackRange(_playerConfig.AttackRange);
        e.AddPosition(Vector3.zero);
        e.AddInitialPosition(new Vector3(0, 0, 0));
        e.AddMove(Vector3.zero);
        e.AddSpeed(_playerConfig.Speed);
        e.AddDirection(0);

        GameController.Player = e;
    }
}
