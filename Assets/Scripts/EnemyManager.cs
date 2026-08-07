using System;
using UnityEngine;

public class EnemyManager : IEntity
{
    private string _name = "DefaultEnemy";
    private int _health = 20;

    private GameObject _visual = default;
    private Rigidbody _rb;
    private BoxCollider _boxCollider;
    private BoxCollider _triggerCollider;
    private MeshRenderer _meshRenderer;
    private MeshFilter _meshFilter;

    [SerializeField] private Mesh targetMesh;

    public void OnStart()
    {
        Debug.Log("IVE STARTED");
        ConfigureUnityComponents();
    }

    public void OnUpdate()
    {
        Debug.Log("Yay the enemy is being updated");
    }
        
    private void FixedUpdate()
    {

    }

    private void ConfigureUnityComponents()
    {
        _visual = new GameObject(_name);
        _visual.tag = "Enemy";
            
        _rb = _visual.AddComponent<Rigidbody>();
        _rb.isKinematic = true;
        _rb.useGravity = false;

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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("I'm colliding with the player!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"I've hit the {other} player!"); 
        }
    }
}   