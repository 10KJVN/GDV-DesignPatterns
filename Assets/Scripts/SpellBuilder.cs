using UnityEngine;

public class SpellBuilder
{
    private string _name = "default";
    private int _health = 100;
    private float _speed = 5f;
    private int _damage = 10;

    public SpellBuilder() // Default spell if none is built.
    {
        _name = "dummy";
        _health = 100;
        _speed = 10f;
        _damage = 10;
    }

    // TODO: Add Build functions for adding a Visual

    public SpellBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public SpellBuilder WithHealth(int health)
    {
        _health = health;
        return this;
    }

    public SpellBuilder WithSpeed(float speed)
    {
        _speed = speed;
        return this;
    }

    public SpellBuilder WithDamage(int damage)
    {
        _damage = damage;
        return this;
    }

    public Spell Build()
    {
        var spell = new Spell();

        spell.Name = _name;
        //spell.Health = _health;
        spell.Speed = _speed;
        spell.Damage = _damage;

        return spell;
    }
}