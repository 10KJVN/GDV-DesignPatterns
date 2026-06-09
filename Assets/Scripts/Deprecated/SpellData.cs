using UnityEngine;

/// <summary>
/// This class' purpose is to serve as Metadata.
/// Providing core information UI and Gameplay systems need, 
/// to track and display the spell.
/// e.g. identity, effect, cost.
/// </summary>

public class SpellData : ScriptableObject
{
    public string Id { get; private set; }
    public int ManaCost { get; private set; }
    public SpellEffect[] Effects { get; private set; } // Damage
}

public abstract class SpellEffect : ScriptableObject
{
    public abstract ISpellStrategy CreateStrategy();
}
