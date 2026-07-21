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
        _visual.AddComponent<Rigidbody>();
        _visual.AddComponent<BoxCollider>();

        //this._triggerCollider

    }
}   