using Entitas;
using Entitas.Unity;
using System.Linq;
using UnityEngine;

public sealed class SpawnZombieSystem : IExecuteSystem
{
    private GameContext _context;

    private Vector2 _spawnInterval = new Vector2(4, 6);
    private float _currentSpawnInterval;
    private float _prevSpawnTime;
    private CharacterConfig _zombieConfig;

    public SpawnZombieSystem(Contexts contexts)
    {
        _zombieConfig = GameSettings.CharStats.FirstOrDefault(x => x.Type == CharacterType.Zombie);
        if (_zombieConfig == null)
        {
            throw new System.Exception("Zombie config is missing");
        }

        _context = contexts.game;
        SetCurrentInterval();
        _prevSpawnTime = Time.time;
    }

    public void Execute()
    {
        if (Time.time - _prevSpawnTime >= _currentSpawnInterval)
        {
            _prevSpawnTime = Time.time;
            SetCurrentInterval();

            CreateZombie();
        }
    }

    private void SetCurrentInterval()
    {
        _currentSpawnInterval = Random.Range(_spawnInterval.x, _spawnInterval.y);
    }

    private void CreateZombie()
    {
        var e = _context.CreateEntity();
        e.isZombie = true;
        e.AddPrefab("Zombie");
        e.AddHealth(_zombieConfig.Health);
        e.AddDamage(_zombieConfig.Damage);
        e.AddAttackRange(_zombieConfig.AttackRange);
        e.AddAttackRate(_zombieConfig.AttackRate);
        e.AddPosition(Vector3.zero);
        e.AddInitialPosition(new Vector3(Random.Range(8f, 9f), 1f, Random.Range(8f, 9f)));
        e.AddMove(Vector3.zero);
        e.AddSpeed(_zombieConfig.Speed);
        e.AddDirection(0);
    }
}
