using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using System.Linq;
using Unity.VisualScripting;

public class Enemy : IDamagable
{
    public int health = 50;
    public System.Action<TestEnemy> OnDeathEvent;

    private readonly List<IEffect> _activeEffects = new();

    private GameObject _spawnedInstance;

    public Enemy(GameObject prefab)
    {
        _spawnedInstance = GameObject.Instantiate(prefab);
    }

    void Start()
    {
        Debug.Log("SPAWNED");
    }

    void OnDestroy()
    {
        Debug.Log("DESTROYED");
    }

    //public void Tick(float delta)
    //{
    //    foreach (This)
    //    {
    //        enemy.OnDeathEvent?.Invoke(this);
    //    }
    //}

    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log($"OH MY GOD!! {amount} DMG. HP: {health}");

        if (health <= 0)
        {
            Die();
        }
    }

    public void ApplyEffect(IEffect effect)
    {
        if (health <= 0) return;
        
        //effect.OnCompleted += RemoveEffect;
        _activeEffects.Add(effect);
        effect.Apply(this);
    }

    private void RemoveEffect(IEffect effect)
    {
        //effect.OnCompleted -= RemoveEffect;
        _activeEffects.Remove(effect);
    }

    public void Die()
    {
        Debug.Log("DIED");

        for (int i = _activeEffects.Count - 1; i >= 0; i--)
        {
            var effect = _activeEffects[i];
            //effect.OnCompleted -= RemoveEffect;
            //effect.Cancel();
        }
        _activeEffects.Clear();
        
        //Destroy(gameObject);
    }
}

public class EnemyManager
{
    public GameObject reference;
    private List<Enemy> _activeEnemies;

    public void SpawnEnemy()
    {
        var e = new Enemy(reference);
        _activeEnemies.Add(e);
    }

    
}

public class TestEnemy
{
    public System.Action<TestEnemy> OnDeathEvent;
    private GameObject spawnedInstance;
    public TestEnemy(GameObject prefab)
    {
        spawnedInstance = GameObject.Instantiate(prefab);
    }

    public void Tick(float delta)
    {
        if (spawnedInstance != null)
        {
            spawnedInstance.transform.position += spawnedInstance.transform.forward * 3f * delta;
        }

        OnDeathEvent?.Invoke(this);

        foreach(var enemy in Registry<TestEnemy>.GetItems((x)=>Vector3.Distance(x.spawnedInstance.transform.position, spawnedInstance.transform.position) < 10))
        {
            enemy.OnDeathEvent?.Invoke(this);
        }
    }
}

public class TestEnemyManager
{
    private ObjectPool<TestEnemy> pool;

    public GameObject reference;

    private List<TestEnemy> activeEnemies;
    public TestEnemyManager() 
    {
    }

    void OnValidate()
    {
        if(reference != null)
        {
            var result = reference.GetComponent<IDamagable>();
            if (result is null)
            {
                reference = null;
            }
        }
    }

    public void SpawnEnemy()
    {
        //TestEnemy enemy = pool.Request();
        //activeEnemies.Add(enemy);
        //enemy.OnDeathEvent += ReturnToPool;
        //Registry<TestEnemy>.AddItem(enemy);
    }

    private void ReturnToPool(TestEnemy enemy)
    {
        activeEnemies.Remove(enemy);
        enemy.OnDeathEvent -= ReturnToPool;
        pool.Release(enemy);
    }
}

public static class Registry<T>
{
    public static List<T> Items = new List<T>();
    public static void AddItem(T item) => Items.Add(item);
    public static void RemoveItem(T item) => Items.Remove(item);
    public static List<T> GetItems(System.Func<T, bool> predicate = null) => Items.FindAll(x => predicate(x));
}

//public class Player : ISuperUniquePlayer, IDamagable
//{
//    public Player()
//    {
//        Registry<ISuperUniquePlayer>.AddItem(this);
//        Registry<Player>.AddItem(this);
//        Registry<IDamagable>.AddItem(this);
//        var players = Registry<ISuperUniquePlayer>.GetItems();
//    }
//}

//public interface ISuperUniquePlayer
//{

//}