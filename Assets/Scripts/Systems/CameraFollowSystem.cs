using Entitas;
using System.Collections.Generic;

public sealed class CameraFollowSystem : ReactiveSystem<GameEntity>
{
    private IGroup<GameEntity> _cameras;

    public CameraFollowSystem(Contexts contexts) : base(contexts.game)
    {
        _cameras = contexts.game.GetGroup(GameMatcher.Camera);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Position);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isPlayer;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            foreach (var c in _cameras.GetEntities())
            {
                var newPosition = e.position.Value;
                newPosition.y = c.camera.CameraTransform.position.y;
                c.camera.CameraTransform.position = newPosition;
            }
        }
    }
}
