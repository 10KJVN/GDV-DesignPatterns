using System;
using UnityEngine;

public class SpellData : ScriptableObject
{
    public string Id => Id;
    public float ManaCost => ManaCost;
    public SpellEffect[] Effects => effects;

    [SerializeField] private string id;
    [SerializeField] private float manaCost;
    [SerializeField] private SpellEffect[] effects;
}

public abstract class SpellEffect : ScriptableObject
{
    public abstract ISpellStrategy CreateStrategy();
}
