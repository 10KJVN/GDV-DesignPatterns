using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private SpellStrategy[] spells;

    void Start()
    {
        CastSpell(0);
    }

    void CastSpell(int index)
    {
        spells[index].CastSpell(transform);
    }
}
