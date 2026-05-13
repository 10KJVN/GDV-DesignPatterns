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

public class Thunderbolt : ISpell
{
    public void Cast()
    {
        Debug.Log("THUNDERBOLT!!");
    }
}

public abstract class SpellFactory
{
    public abstract ISpell CreateSpell();
}

public class FireFactory : SpellFactory
{
    public override ISpell CreateSpell()
    {
        return new Fireball();
    }
}
