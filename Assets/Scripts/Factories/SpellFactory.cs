using UnityEngine;

public interface ISpell
{
    void Cast();
    
    static ISpell CreateDefault()
    {
        return new Fireball();
    }
}

public class Fireball : ISpell
{
    public void Cast()
    {
        Debug.Log("FIREBALL!");
    }
}

public class Iceball : ISpell
{
    public void Cast()
    {
        Debug.Log("ICEBALL!");
    }
}

public class Thunderbolt : ISpell
{
    public void Cast()
    {
        Debug.Log("THUNDERBOLT!!");
    }
}

public abstract class SpellFactory : ScriptableObject
{
    public abstract ISpell CreateSpell();
}