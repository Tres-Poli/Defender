using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Input]
public class MoveInputComponent : IComponent
{
    public Vector3 Value;
}
