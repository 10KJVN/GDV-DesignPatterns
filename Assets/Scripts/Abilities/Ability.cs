using System;
using System.Collections.Generic;
using UnityEngine;

namespace Abilities
{
    [Serializable]
    public class Ability
    {
        public AudioClip castSfx;
        public GameObject castVfx;
        public GameObject runningVfx;
        // caster information maybe
        
        [SerializeReference] public List<IEffect<IDamagable>> effects = new();

        public void Execute(IDamagable target)
        {
            foreach (var effect in effects)
            {
                if (target is Enemy enemy)
                {
                    enemy.ApplyEffect(effect);
                }
                else
                {
                    effect.Apply(target);
                }
            }
        }
    }
}