using System;
using Abilities;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Ability[] hotbar;
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
        for (int i = 0; i < hotbar.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                // TODO: Replace with real targeting system
                hotbar[i].Execute(FindFirstObjectByType<Enemy>()); 
            }
        }
    }

    void CastSpell(int index)
    {
        spells[index].CastSpell(transform);
    }
}
