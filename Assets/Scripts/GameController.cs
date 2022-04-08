using Entitas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameEntity _player;
    public static GameEntity Player 
    {
        get
        {
            return _player;
        }
        set
        {
            _player = value;
            PlayerAssigned?.Invoke();
        }
    }

    public static event Action PlayerAssigned;

    private Systems _systems;
    private Systems _lateUpdateSystems;

    private ConfigLoader<LevelProgressionConfig> _lvlProgressionLoader;
    private ConfigLoader<CharacterConfig[]> _charStatsLoader;

    void Start()
    {
        var e = Contexts.sharedInstance.game.CreateEntity();
        e.AddCamera(Camera.main.transform);

        _systems = AddSystems(Contexts.sharedInstance);
        _lateUpdateSystems = AddLateUpdateSystems(Contexts.sharedInstance);
        _systems.Initialize();
    }

    void Update()
    {
        _systems.Execute();
    }

    private void LateUpdate()
    {
        _lateUpdateSystems.Execute();
    }

    private Systems AddSystems(Contexts contexts)
    {
        return new Feature()
            .Add(new ViewSystems(contexts))
            .Add(new TransformSystems(contexts))
            .Add(new InitSystems(contexts))
            .Add(new InputSystems(contexts))
            .Add(new GameSystems(contexts))
            .Add(new ZombieAISystems(contexts))
            .Add(new PlayerSystems(contexts));
    }

    private Systems AddLateUpdateSystems(Contexts contexts)
    {
        return new Feature()
            .Add(new CameraFollowSystem(contexts));
    }
}
