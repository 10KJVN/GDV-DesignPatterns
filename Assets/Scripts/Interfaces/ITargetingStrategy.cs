using UnityEngine;

public interface ITargetingStrategy
{
    Transform[] GetTargets(Transform caster);
}