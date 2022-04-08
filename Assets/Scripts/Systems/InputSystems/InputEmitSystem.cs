using Entitas;
using UnityEngine;

public sealed class InputEmitSystem : IExecuteSystem
{
    private readonly InputContext _context;

    private readonly InputEntity _inputEntity;

    public InputEmitSystem(Contexts contexts)
    {
        _context = contexts.input;

        _inputEntity = _context.CreateEntity();
        _inputEntity.AddMoveInput(Vector3.zero);
        _inputEntity.isAttackInput = false;
    }

    public void Execute()
    {
        ExecuteMoveChecks();
    }

    private void ExecuteMoveChecks()
    {
        var moveDir = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            moveDir += Vector3.forward;
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveDir += Vector3.left;
        }

        if (Input.GetKey(KeyCode.S))
        {
            moveDir += Vector3.back;
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveDir += Vector3.right;
        }

        _inputEntity.ReplaceMoveInput(moveDir);
    }

    private void ExecuteAttackChecks()
    {

    }
}
