using Entitas;

public sealed class ViewSystems : Feature
{
    public ViewSystems(Contexts contexts) : base("View systems")
    {
        Add(new AddViewSystem(contexts));
        Add(new AnimationMoveSystem(contexts));
        Add(new AnimationAttackSystem(contexts));
        Add(new AnimationIdleSystem(contexts));
    }
}
