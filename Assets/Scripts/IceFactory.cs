using UnityEngine;

[CreateAssetMenu(fileName = "IceFactory", menuName = "Spell Factory/Ice")]
public class IceFactory : SpellFactory
{
    public override ISpell CreateSpell()
    {
        return new Iceball();
    }
}