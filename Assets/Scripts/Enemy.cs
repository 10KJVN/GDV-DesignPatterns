using UnityEngine;

public class Enemy : IDamagable
{
    public int health = 50;
    
    public void TakeDamage(int damage)
    {
        Debug.Log($"OH MY GOD!! {damage}");
    }

    public void Die()
    {
        Debug.Log("DIED");
    }
}