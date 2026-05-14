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

    private void Start()
    {
        _spellFactory = new FireFactory();
        _spell = _spellFactory?.CreateSpell();
        
        Cast();
    }

    private void Cast() => _spell?.Cast();
    
}