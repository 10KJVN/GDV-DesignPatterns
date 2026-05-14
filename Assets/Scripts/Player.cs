using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private SpellStrategy[] spells;

    void Start()
    {
        //CastSpell(2);
        //CastSpell(1);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CastSpell(0);
        }
        
        else if (Input.GetMouseButtonDown(1))
        {
            CastSpell(1);
        }
        
        else if (Input.GetMouseButtonDown(2))
        {
            CastSpell(2);
        }
    }

    void CastSpell(int index)
    {
        spells[index].CastSpell(transform);
    }
}
