using System;
using System.Collections.Generic;
using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(fileName = "AbilityData", menuName = "ScriptableObjects/AbilityData")]
    internal class AbilityData : ScriptableObject
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
    internal abstract class AbilityEffect
    {
        public abstract void Execute(GameObject caster, GameObject target);
    }
    
    [Serializable]
    internal class DamageEffect : AbilityEffect
    {
        [SerializeField] public int amount;
        
        public override void Execute(GameObject caster, GameObject target)
        {
            //target.GetComponent<Health>().ApplyDamage(amount);
            Debug.Log($"{caster.name} dealt {amount} damage to {target.name}");
        }
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