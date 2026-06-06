using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    public int health = 50;

    private readonly List<IEffect<IDamagable>> _activeEffects = new();

    void Start()
    {
        Debug.Log("SPAWNED (PROBABLY)");
    }

    void OnDestroy()
    {
        Debug.Log("DESTROYED");
    }
    
    void SpawnEnemy(int index)
    {
        GameObject enemyGO = new GameObject("TestEnemy");
        Enemy enemy = enemyGO.AddComponent<Enemy>();
        
        
        enemy = new Enemy().GetComponent<Enemy>();
    }
    
    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log($"OH MY GOD!! {amount} DMG. HP: {health}");

        if (health <= 0)
        {
            Die();
        }
    }

    public void ApplyEffect(IEffect<IDamagable> effect)
    {
        effect.OnCompleted += RemoveEffect;
        _activeEffects.Add(effect);
        effect.Apply(this);
    }

    private void RemoveEffect(IEffect<IDamagable> effect)
    {
        effect.OnCompleted -= RemoveEffect;
        _activeEffects.Remove(effect);
    }

    public void Die()
    {
        Debug.Log("DIED");

        foreach (var effect in _activeEffects)
        {
            effect.OnCompleted -= RemoveEffect;
            effect.Cancel();
        }
        _activeEffects.Clear();
        
        Destroy(gameObject);
    }
}