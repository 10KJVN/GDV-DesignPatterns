using System;
using UnityEngine;

public class EnemyManager : IEntity
{
    private string _name = "DefaultEnemy";
    private int _health = 20;

    private GameObject _visual = default;
    private Rigidbody _rb;
    private BoxCollider _boxCollider;
    private BoxCollider _triggerCollider; // TODO: SET TRIGGER IS TRUE AND CONFIG IN CODE.
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
        //throw new NotImplementedException();
    }

    private void ConfigureUnityComponents()
    {
        _visual = new GameObject(_name);

        _rb = _visual.AddComponent<Rigidbody>();
        _rb.isKinematic = true;
        _rb.useGravity = false;

        _boxCollider = _visual.AddComponent<BoxCollider>();
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
}   