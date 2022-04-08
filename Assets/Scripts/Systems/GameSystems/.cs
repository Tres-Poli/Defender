using Entitas;
using System.Collections.Generic;

public sealed class CameraFollowSystem : ReactiveSystem<GameEntity>
{
    private IGroup<GameEntity> _cameraGroup;

    public CameraFollowSystem(Contexts contexts) : base(contexts.game)
    {
        //_cameraGroup = contexts.game.GetGroup(GameMatcher.);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.Player));
    }

    protected override bool Filter(GameEntity entity)
    {
        throw new System.NotImplementedException();
    }

    protected override void Execute(List<GameEntity> entities)
    {
        throw new System.NotImplementedException();
    }
}
