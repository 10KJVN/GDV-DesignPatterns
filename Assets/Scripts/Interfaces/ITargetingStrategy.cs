using UnityEngine;

public interface ITargetingStrategy
{
    Transform[] GetTargets(Transform caster);
    CombatContext GetCombatContext(Transform caster, Transform target);
}