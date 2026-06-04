using UnityEngine;

public interface IMovementComponent
{
    void Move(Transform caster, Transform target, System.Action onArrival);
}