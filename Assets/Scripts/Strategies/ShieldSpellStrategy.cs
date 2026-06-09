using UnityEngine;

[CreateAssetMenu(fileName = "ShieldSpellStrategy", menuName = "Spells/ShieldSpellStrategy")]
public class ShieldSpellStrategy : SpellStrategy
{
    public GameObject shieldPrefab;
    public float duration = 10f;
    
    public override void CastSpell(Transform origin)
    {
        var shield = Instantiate(shieldPrefab, origin.position, Quaternion.identity, origin);
        Destroy(shield, duration);
    }
}