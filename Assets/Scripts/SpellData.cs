using UnityEngine;

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
