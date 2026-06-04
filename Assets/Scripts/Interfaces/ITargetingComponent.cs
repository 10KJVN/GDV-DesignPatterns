using UnityEngine;

public interface ITargetingComponent
{
    Transform[] GetTargets(Transform caster);
}