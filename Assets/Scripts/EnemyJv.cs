using System.Collections.Generic;
using UnityEngine;

public class EnemyJv : MonoBehaviour, IDamagable
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
        EnemyJv enemy = enemyGO.AddComponent<EnemyJv>();
        
        
        enemy = new EnemyJv().GetComponent<EnemyJv>();
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
        if (health <= 0) return;
        
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

        for (int i = _activeEffects.Count - 1; i >= 0; i--)
        {
            var effect = _activeEffects[i];
            effect.OnCompleted -= RemoveEffect;
            effect.Cancel();
        }
        _activeEffects.Clear();
        
        Destroy(gameObject);
    }
}