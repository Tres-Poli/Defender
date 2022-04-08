using Entitas;
using Entitas.Unity;
using System.Collections.Generic;
using UnityEngine;

public sealed class AddViewSystem : ReactiveSystem<GameEntity>
{
    private Transform _gameViewRoot = new GameObject("GameViewRoot").transform;

    public AddViewSystem(Contexts contexts) : base(contexts.game)
    {

    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Prefab);
    }

    protected override bool Filter(GameEntity entity)
    {
        return !entity.hasView;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            var go = Object.Instantiate(Resources.Load<GameObject>(e.prefab.PrefabName));
            go.transform.SetParent(_gameViewRoot, false);
            if (e.hasInitialPosition && e.hasPosition)
            {
                e.ReplacePosition(e.initialPosition.Value);
            }

            e.AddView(go);
            go.Link(e);
        }
    }
}
