using UnityEngine;

public class Spell
{
    // BASIC PROPERTIES
    public string Name { get; set; }
    public int ManaCost { get; set; }
    public int Damage { get; set; }

    public enum ElementType { Fire, Ice, Thunder }
    public float Speed { get; set; }

    public Spell() // Default CTOR
    {
        //Cast();
    }

    public void Cast()
    {
        Debug.Log($"Casting {Name}! It deals {Damage}");
    }

}
