using System;
using System.Collections.Generic;
using UnityEngine;

namespace Abilities
{
    [Serializable]
    public class Ability
    {
        [SerializeReference] public List<IEffect<IDamagable>> effects = new();

        public void Execute(IDamagable target)
        {
            foreach (var effect in effects)
            {
                effect.Apply(target);
            }
        }
    }
}