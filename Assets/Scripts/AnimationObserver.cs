using Entitas;
using System;
using System.Collections;
using UnityEngine;

public sealed class AnimationObserver : MonoBehaviour
{
    private GameEntity _target;
    private Action _action;
    private Vector3 _startPos;
    private GameEntity _entity;

    public void Init(GameEntity e)
    {
        _entity = e;
    }

    public void SetOnAttackCast(GameEntity target, Action action, Vector3 startPos)
    {
        _target = target;
        _action = action;
        _startPos = startPos;
    }

    public void OnAttackCast()
    {
        if (_target != null && _action != null && _target.hasPosition)
        {
            var missile = Instantiate(Resources.Load<GameObject>("ShadowMissile"));
            missile.transform.position = _startPos;
            StartCoroutine(MissileMove(_target, missile.transform, _action));
        }
    }

    public void OnAttackComplete()
    {
        _entity.isAttacking = false;
    }

    private IEnumerator MissileMove(GameEntity target, Transform missile, Action action)
    {
        while ((target.position.Value - missile.position).magnitude > 0.25f)
        {
            var missileDir = (target.position.Value - missile.position).normalized;
            missile.position = missile.position + missileDir * Time.deltaTime * 10;
            yield return null;
        }

        action.Invoke();
    }
}
