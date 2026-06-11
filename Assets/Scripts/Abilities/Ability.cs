using System;
using System.Collections.Generic;
using Extensions;
using Strategies;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Abilities
{
    [Serializable]
    public class Ability
    {
        public AudioClip castSfx;
        public GameObject castVfx;
        public GameObject runningVfx;
        // caster information maybe
        
        [Header("Effects")]
        [SerializeReference] public List<IEffectFactory> effects = new();
        public List<AbilityData> effectData = new(); // TODO: Refactor to WORKING ScriptableObject
        
        [Header("Targeting")]
        [SerializeReference] private TargetingStrategy targetingStrategy;
        // TODO: Refactor to ScriptableObject

        public void Target(TargetingManager targetingManager)
        {
            if (targetingStrategy != null)
            {
                targetingStrategy.Start(this, targetingManager);
            }
        }

        public void Execute(IDamagable target)
        {
            HandleVFX(target);
            
            foreach (var effect in effects)
            {
                var runtimeEffect = effect.Create();
                target.ApplyEffect(runtimeEffect);
            }
        }

        private void HandleVFX(IDamagable target)
        {
            var targetMb = target as MonoBehaviour;
            if (targetMb == null) return;

            if (castVfx != null)
            {
                Object.Instantiate(castVfx, targetMb.transform.position.Add(y:2), Quaternion.identity);
            }

            if (runningVfx != null)
            {
                var runningVfxInstance = Object.Instantiate(runningVfx, targetMb.transform);
                Object.Destroy(runningVfxInstance, 3f);
            }
        }
    }
}

public abstract class Test
{
    protected GameObject instance;
    public Test(GameObject obj)
    {
        instance = obj;
    }
}

public class SubTest : Test
{
    public SubTest(GameObject obj) : base(obj)
    {

    }
}