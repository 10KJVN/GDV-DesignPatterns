using UnityEngine;

public interface IEffectComponent
{
    void ApplyEffect(Transform caster, Transform target);
}