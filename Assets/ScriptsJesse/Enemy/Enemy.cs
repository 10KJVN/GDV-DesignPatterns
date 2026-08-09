using System;
using UnityEngine;

public class Enemy
{
    private ILoot lootStrategy;


    public EnemyDefinition Definition
    {
        get;
        private set;
    }


    public GameObject GameObject
    {
        get;
        private set;
    }


    public Vector3 Position
    {
        get
        {
            if (GameObject != null)
            {
                return GameObject.transform.position;
            }

            return Vector3.zero;
        }
    }


    public event Action OnDeath;


    public Enemy(EnemyDefinition definition, GameObject gameObject)
    {
        Definition = definition;

        GameObject = gameObject;

        lootStrategy = definition.lootStrategy;
    }


    public LootResult GetLoot()
    {
        return lootStrategy.GenerateLoot();
    }


    public void Die()
    {
        Debug.Log(Definition.enemyName + " died.");

        OnDeath?.Invoke();
    }
}