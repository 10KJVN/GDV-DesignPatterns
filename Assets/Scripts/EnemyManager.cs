using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class EnemyManager : IEntity
{
    private string _name = "DefaultEnemy";
    private float _health = 20;

    private GameObject _visual = default;
    private Rigidbody _rb;
    private BoxCollider _boxCollider;
    private BoxCollider _triggerCollider;
    private MeshRenderer _meshRenderer;
    private MeshFilter _meshFilter;
    
    public void OnStart()
    {
        Debug.Log("IVE STARTED");
        ConfigureUnityComponents();
    }

    public void OnUpdate()
    {
        if (_health <= 0) return;
        //Debug.Log("Yay the enemy is being updated");
        Collider[] hitColliders = Physics.OverlapSphere(_visual.transform.position, 1.0f);

        foreach (var hit in hitColliders)
        {
            if (hit.CompareTag("Spell"))
            {
                Debug.Log($"I've been hit by: {hit.name}");
                TakeDamage(10); // Better comms later maybe
                Object.Destroy(hit.gameObject);
                break;
            }
        }
    }

    private void ConfigureUnityComponents()
    {
        _visual = new GameObject(_name);
        _visual.tag = "Enemy";
            
        _rb = _visual.AddComponent<Rigidbody>();
        _rb.isKinematic = true;
        _rb.useGravity = false;
        _rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        _boxCollider = _visual.AddComponent<BoxCollider>();
        _boxCollider.size = new Vector3(1f, 1.8f, 1f);

        ConfigureTrigger();
    }

    public void ConfigureMesh(Mesh target)
    {
        _meshFilter = _visual.AddComponent<MeshFilter>();
        _meshFilter.mesh = target;

        Debug.Log("Mesh configured sucessfully.");
    }

    public void AssignMaterial(Material target)
    {
        _meshRenderer = _visual.AddComponent<MeshRenderer>();
        _meshRenderer.material = target;

        Debug.Log("Material assigned");
    }

    private void ConfigureTrigger()
    {
        _triggerCollider = _visual.AddComponent<BoxCollider>();
        _triggerCollider.isTrigger = true;
        _triggerCollider.size = new Vector3(1.5f, 1.5f, 1.5f);
    }

    private void TakeDamage(float damage)
    {
        _health -= damage;
        Debug.Log($"HP: {_health} LEFT");
        
        if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Oh no, i've died.");
        Object.Destroy(_visual);
    }
}   