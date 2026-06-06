using System;
using Abilities;
using Extensions;
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
                // hotbar[i].Execute(FindFirstObjectByType<Enemy>()); 
                Cast(hotbar[i], FindFirstObjectByType<Enemy>()); 
            }
        }
    }

    void Cast(Ability ability, IDamagable target)
    {
        ability.Execute(target);
        
        var targetMb = target as MonoBehaviour;

        if (ability.castVfx && targetMb)
        {
            var vfx = Instantiate(ability.castVfx, targetMb.transform.position.With(y:2), Quaternion.identity);
            Destroy(vfx, 3f);
        }

        if (ability.runningVfx && targetMb)
        {
            var runningVfxInstance = Instantiate(ability.runningVfx, targetMb.transform);
            Destroy(runningVfxInstance, 3f);
        }

        if (ability.castSfx)
        {
            AudioSource.PlayClipAtPoint(ability.castSfx, transform.position);
        }
            
    }

    void CastSpell(int index)
    {
        spells[index].CastSpell(transform);
    }
}
