using System;
using System.Collections.Generic;
using ImprovedTimers;
using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(fileName = "AbilityData", menuName = "ScriptableObjects/AbilityData")]
    public class AbilityData : ScriptableObject
    {
        public string label;
        [SerializeReference] public List<AbilityEffect> effects;
        
        // public AnimationClip animationClip;
        [Range(0.1f, 4f)] public float castTime = 2f;
        public ProjectileMover vfxPrefab;
        //public GameObject vfxPrefab;

        private void OnEnable()
        {
            // if label hasn't been set yet, give it the final name.
            if (string.IsNullOrEmpty(label)) label = name;
        
            // if the list hasn't been initialized yet, set an empty list.
            if (effects == null) effects = new List<AbilityEffect>();
        }
    }
    
    /// <summary>
    /// Base class for all the effects.
    /// </summary>
    [Serializable]
    public abstract class AbilityEffect
    {
        public abstract void Execute(GameObject caster, GameObject target);
    }

    public interface IEffectFactory<TTarget>
    {
        IEffect<TTarget> Create();
    }
    
    [Serializable]
    public class DamageEffectFactory : IEffectFactory<IDamagable>
    {
        public int damageAmount = 10;

        public IEffect<IDamagable> Create()
        {
            return new DamageEffect { damageAmount = damageAmount };
        }
    }

    [Serializable]
    public struct DamageEffect : IEffect<IDamagable>
    {
        public int damageAmount;
        
        // public override void Execute(GameObject caster, GameObject target)
        // {
        //     //target.GetComponent<Health>().ApplyDamage(amount);
        //     Debug.Log($"{caster.name} dealt {damageAmount} damage to {target.name}");
        // }

        public void Apply(IDamagable target)
        {
            target.TakeDamage(damageAmount);
            OnCompleted?.Invoke(this);
        }

        public void Cancel()
        {
            OnCompleted?.Invoke(this);
        }

        public event Action<IEffect<IDamagable>> OnCompleted;
    }

    [Serializable]
    public class DamageOverTimeEffectFactory : IEffectFactory<IDamagable>
    {
        public float duration = 5;
        public float tickInterval = 1f;
        public int damagePerTick = 5;
        
        public IEffect<IDamagable> Create()
        {
            return new DamageOverTimeEffect
            {
                duration = duration,
                tickInterval = tickInterval,
                damagePerTick = damagePerTick
            };
        }
    }

    [Serializable]
    public struct DamageOverTimeEffect : IEffect<IDamagable>
    {
        public float duration;
        public float tickInterval;
        public int damagePerTick;

        private IntervalTimer _timer;
        private IDamagable _currentTarget;
        
        public void Apply(IDamagable target)
        {
            _currentTarget = target;
            _timer = new IntervalTimer(duration, tickInterval);
            _timer.OnInterval = OnInterval;
            _timer.OnTimerStop = OnStop;
            _timer.Start();
        }
        
        void OnInterval() => _currentTarget?.TakeDamage(damagePerTick);
        void OnStop() => Cleanup();

        public void Cancel()
        {
            _timer?.Stop();
            Cleanup();
        }

        public event Action<IEffect<IDamagable>> OnCompleted;

        // TODO: Improve by keeping timer cached.
        private void Cleanup()
        {
            _timer = null; 
            _currentTarget = null;
            OnCompleted?.Invoke(this);
        }

        // public override void Execute(GameObject caster, GameObject target)
        // {
        //     Debug.Log($"{caster.name} inflicts {damagePerTick} DMG." +
        //               $" Every {tickInterval} to {target.name}!" +
        //               $" for {duration}s !!");
        // }
    }
    
    [Serializable]
    internal class KnockbackEffect : AbilityEffect
    {
        public float force;

        public override void Execute(GameObject caster, GameObject target)
        {
            var dir = (target.transform.position - caster.transform.position).normalized;
            target.GetComponent<Rigidbody>().AddForce(dir * force, ForceMode.Impulse);
            Debug.Log($"{caster.name} knocked back {target.name} with force {force}");
        }
    }
}