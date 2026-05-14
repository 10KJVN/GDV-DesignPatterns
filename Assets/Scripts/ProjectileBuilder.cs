using Extensions;
using UnityEngine;

public class ProjectileBuilder
{
    private GameObject _projectilePrefab;
    private float _speed;
    private float _duration;
        
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

    // public GameObject Build(Transform origin)
    // {
    //     Vector3 instantiatePosition = origin.position + origin.forward * 2f;
    //     
    //     GameObject fireball = Instantiate(_projectilePrefab, instantiatePosition.With(y: 1), Quaternion.identity);
    //     Rigidbody rb = fireball.GetComponent<Rigidbody>();
    //     rb.linearVelocity = fireball.transform.forward * _speed;
    //     // Get/Add Particle Movement, SelfDestruct
    //         
    //     return fireball;
    // }
}