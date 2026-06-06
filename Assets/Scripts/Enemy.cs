using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    public int health = 50;

    // void Awake()
    // {
    //     Debug.Log("Awake");
    // }
    //
    // void OnEnable()
    // {
    //     Debug.Log("ENABLED");
    //     HeadsUpDisplay.OnButtonPressed += SpawnEnemy;
    // }
    //
    // void OnDisable()
    // {
    //     Debug.Log("DISABLED");
    //     HeadsUpDisplay.OnButtonPressed -= SpawnEnemy;
    // }

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

    public void Die()
    {
        Debug.Log("DIED");
        Destroy(gameObject);
    }
}