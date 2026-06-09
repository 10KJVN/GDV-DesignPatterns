using UnityEngine;

[CreateAssetMenu(fileName = "FireFactory", menuName = "Spell Factory/Fire")]
public class FireFactory : SpellFactory
{
    public override ISpell CreateSpell()
    {
        return new Fireball();
    }
}