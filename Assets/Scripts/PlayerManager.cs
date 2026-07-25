using Abilities;
using Strategies;
using System;
using UnityEngine;

public class PlayerManager : IEntity
{
    private SpellStrategy[] spells;
    private Transform transform;

    private GameObject _visual;
    private Rigidbody _rb;
    private BoxCollider _boxCollider;
    private MeshRenderer _meshRenderer;
    private MeshFilter _meshFilter;
    private Mesh _mesh;

    private string _name = "The Player";

    private void OnEnable()
    {
        HeadsUpDisplay.OnButtonPressed += CastSpell;
    }

    private void OnDisable()
    {
        HeadsUpDisplay.OnButtonPressed -= CastSpell;
    }

    public void OnStart()
    {
        ConfigureUnityComponents();
    }

    public void OnUpdate()
    {
        throw new NotImplementedException();
    }

    private void CastSpell(int index) => spells[index].CastSpell(transform);

    private void ConfigureUnityComponents()
    {
        _visual = new GameObject(_name);
    }

    // TODO: Spawn in the HUD through code
    // And somehow assign the spell strat spells to it
    // e.g. Assets/Scripts/ScriptableObjects/Spells/OrbitalSpellStrategy.asset

    // TODO: Also find a way to make the transform be an actual transform.
}