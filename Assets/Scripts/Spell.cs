using System;
using UnityEngine;

public sealed class Spell : ScriptableObject
{
    // BASIC PROPERTIES
    public string Name { get; set; }
    public int Cost { get; set; }
    public int Damage { get; set; }

    // COMPONENTS
    // public ITargetingComponent targetingComponent;
    // public IMovementComponent movementComponent;
    // public IEffectComponent effectComponent;
    
    // ADDITIONAL PROPERTIES
    public float Speed { get; set; }
    public float Duration { get; set; }
    public float Cooldown { get; set; }
    public GameObject Visual { get; set; }
    public SpellStrategy Strategy { get; set; }
    
    // OPTIONAL PROPERTIES
    public enum ElementType { Fire, Ice, Thunder }
    // TargetType, EffectType, etc. 

    // public Spell()
    // { Start(); }

    public void Cast()
    {
        Debug.Log($"Required Mana: {Cost} MP");
        Debug.Log($"Casting {Name}! It deals {Damage} DMG.");
    }

    public void Cast(Transform caster)
    {
        Debug.Log($"Required Mana: {Cost}");
        Debug.Log($"Casting {Name}! It deals {Damage}");
    }
    
    private SpellData _data;

    public void Execute(IDamagable target)
    {
        foreach (var effect in _data.Effects)
        {
            //effect.Apply(target);
        }
    }

}
