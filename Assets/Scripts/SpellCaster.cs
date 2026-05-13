using System;
using UnityEngine;

public class SpellCaster : MonoBehaviour
{
    private SpellFactory _spellFactory;
    private ISpell _spell = ISpell.CreateDefault();
    
    SpellCaster()
    {
        _spellFactory = new FireFactory();
        _spell = _spellFactory?.CreateSpell();
    }

    private void Start()
    {
        Cast();
    }

    private void Cast() => _spell?.Cast();
    
}