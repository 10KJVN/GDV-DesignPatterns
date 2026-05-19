using UnityEngine;

public class Spell : ISpell
{
    public string Name { get; private set; }
    public int Health { get; private set; }
    public float Speed { get; private set; }
    public float Damage { get; private set; }

    public Spell() // Default CTOR
    {
        //Cast();
    }

    public void Cast()
    {
        Debug.Log("Default Spell");
    }

    public class Builder
    {
        private string _name = "default";
        private int _health = 100;
        private float _speed = 5f;
        private int _damage = 10;

        public Builder() // Default spell if none is built.
        {
            _name = "dummy";
            _health = 100;
            _speed = 10f;
            _damage = 10;
        }

        // TODO: Add Build functions for adding a Visual

        public Builder WithName(string name)
        {
            _name = name;
            return this;
        }

        public Builder WithHealth(int health)
        {
            _health = health;
            return this;
        }

        public Builder WithSpeed(float speed)
        {
            _speed = speed;
            return this;
        }

        public Builder WithDamage(int damage)
        {
            _damage = damage;
            return this;
        }

        public Spell Build()
        {
            var spell = new Spell();

            spell.Name = _name;
            spell.Health = _health;
            spell.Speed = _speed;
            spell.Damage = _damage;
 
            return spell;
        }
    }
}
