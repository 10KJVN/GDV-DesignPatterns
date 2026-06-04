using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    //public Action 

    [SerializeField] private SpellStrategy[] spells;

    private void OnEnable()
    {
        HeadsUpDisplay.OnButtonPressed += CastSpell;
    }

    private void OnDisable()
    {
        HeadsUpDisplay.OnButtonPressed -= CastSpell;
    }

    void Start()
    {
        //CastSpell(2);
        //CastSpell(1);
    }

    private void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    CastSpell(0);
        //}
        
        //else if (Input.GetMouseButtonDown(1))
        //{
        //    CastSpell(1);
        //}
        
        //else if (Input.GetMouseButtonDown(2))
        //{
        //    CastSpell(2);
        //}
    }

    void CastSpell(int index)
    {
        spells[index].CastSpell(transform);
    }
}
