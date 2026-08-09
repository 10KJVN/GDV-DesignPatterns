using Extensions;
using UnityEngine;

public class ProjectileBuilder : ScriptableObject
{
    private GameObject _projectilePrefab;
    private float _speed;
    private float _duration;
    private float _damage;
        
    public ProjectileBuilder WithProjectilePrefab(GameObject prefab)
    {
        _projectilePrefab = prefab;
        return this;
    }

    public ProjectileBuilder WithSpeed(float speed)
    {
        _speed = speed;
        return this;
    }

    public ProjectileBuilder WithDuration(float duration)
    {
        _duration = duration;
        return this;
    }

    public ProjectileBuilder WithDamage(float damage)
    {
        _damage = damage;
        return this;
    }

    //TODO: Implement better SelfDestruct & ParticleMovement
    public GameObject Build(Transform origin)
    {
        Vector3 instantiatePosition = origin.position + origin.forward * 2f;
        
        GameObject fireball = Instantiate(_projectilePrefab, instantiatePosition.With(y: 0.5f), Quaternion.identity);
        Rigidbody rb = fireball.GetComponent<Rigidbody>();
        rb.linearVelocity = fireball.transform.forward * _speed;
        Destroy(fireball, _duration);
            
        return fireball;
    }
}