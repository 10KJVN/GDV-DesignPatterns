using UnityEngine;

public interface IMovementStrategy
{
    void Move(Transform caster, Transform target, System.Action onArrival);
}