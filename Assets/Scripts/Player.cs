using System;
using Abilities;
using Strategies;
using Extensions;
using UnityEngine;

[RequireComponent(typeof(TargetingManager))]
public class Player : MonoBehaviour
{
    public Ability[] hotbar;
    public TargetingManager targetingManager;
    
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
                Cast(hotbar[i]);
            }
        }
    }

    void Cast(Ability ability)
    {
        ability.Target(targetingManager);

        if (ability.castSfx)
        {
            AudioSource.PlayClipAtPoint(ability.castSfx, transform.position);
        }
    }

    void CastSpell(int index) => spells[index].CastSpell(transform);
}
