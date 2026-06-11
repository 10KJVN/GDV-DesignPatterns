using Abilities;
using UnityEngine;

public class SpellBuilder
{
    private string _name = "dummy";
    private int _cost = 5;
    private int _damage = 10;
    private float _speed = 3;

    // TODO: Add Build functions for adding a Visual
    IEffectFactory damageEffect;

    //IEffectFactory<DamageOverTimeEffect> dotFactory;
    public SpellBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public SpellBuilder WithCost(int cost)
    {
        _cost = cost;
        return this;
    }
    
    public SpellBuilder WithDamage(int damage)
    {
        _damage = damage;
        return this;
    }
    
    public SpellBuilder WithSpeed(float speed)
    {
        _speed = speed;
        return this;
    }

    public SpellBuilder WithFactory(IEffectFactory damageEffect)
    {
        this.damageEffect = damageEffect;
        return this;
    }

    public Spell Build()
    {
        //var spell = new Spell();
        var spell = ScriptableObject.CreateInstance<Spell>();
        //var spell = new GameObject().GetComponent<Spell>();
        //var spell = new GameObject("Spell").AddComponent<Spell>();
    
        spell.Name = _name;
        spell.Cost = _cost;
        spell.Damage = _damage;
        spell.Speed = _speed;
        //spell.damageEffect = ScriptableObject.CreateInstance<AbilityData>();
    
        return spell;
    }
}