using UnityEngine;

public class Spell
{
    // BASIC PROPERTIES
    public string Name { get; set; } // Identity
    public int ManaCost { get; set; } // Cost
    public int Damage { get; set; } // Effect

    // ADDITIONAL PROPERTIES
    public float Speed { get; set; }
    
    // OPTIONAL PROPERTIES
    public enum ElementType { Fire, Ice, Thunder }
    // TargetType, EffectType, etc.

    public Spell() // Default CTOR
    {
        //Cast();
    }

    public void Cast()
    {
        Debug.Log($"Required Mana: {ManaCost}");
        Debug.Log($"Casting {Name}! It deals {Damage}");
    }

}
