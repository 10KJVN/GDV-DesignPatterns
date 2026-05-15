using UnityEngine;

[CreateAssetMenu(fileName = "ThunderFactory", menuName = "Spell Factory/Thunder")]
public class ThunderFactory : SpellFactory
{
    public override ISpell CreateSpell()
    {
        return new Thunderbolt();
    }
}