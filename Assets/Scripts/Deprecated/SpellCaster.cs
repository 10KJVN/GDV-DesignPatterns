using System;
using UnityEngine;

public class SpellCaster : MonoBehaviour
{
    [SerializeField] private SpellFactory _spellFactory;
    private ISpell _spell = ISpell.CreateDefault();

    // private SpellCaster()
    // {
    //     _spellFactory = new FireFactory();
    //     _spell = _spellFactory?.CreateSpell();
    // }

    // TODO: Rethink auto-assigning an element on start.
    private void Start()
    {
        _spell = _spellFactory?.CreateSpell(); // Assigns selected Factory spell
        
        Cast();
    }

    private void Cast() => _spell?.Cast();
    
}