using System;
using System.Collections.Generic;
//using ImprovedTimers;
using UnityEngine;

public interface IEffect<TTarget>
{
    void Apply(TTarget target);
    void Cancel();
}

[Serializable]
public class DamageEffect : IEffect<Enemy>
{
    public int damageAmount = 10;

    public void Apply(Enemy target)
    {
        target.TakeDamage(damageAmount);
    }

    public void Cancel()
    {
        // no-op
    }
}