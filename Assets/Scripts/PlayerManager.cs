using Abilities;
using Strategies;
using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class PlayerManager : IEntity
{
    private SpellStrategy[] spells;

    private GameObject _visual;
    private Rigidbody _rb;
    private BoxCollider _boxCollider;
    private BoxCollider _triggerCollider;

    private MeshRenderer _meshRenderer;
    private MeshFilter _meshFilter;
    private Mesh _mesh;

    private string _name = "The Player";
    private float _health = 500f;

    private void OnEnable()
    {
        HeadsUpDisplay2.OnButtonPressed2 += CastSpell;
    }

    private void OnDisable()
    {
        HeadsUpDisplay2.OnButtonPressed2 -= CastSpell;
    }

    ~PlayerManager()
    {
        OnDisable();
    }

    public PlayerManager(SpellStrategy[] spellRef)
    {
        spells = spellRef;
    }

    public void OnStart()
    {
        OnEnable();
        ConfigureUnityComponents();
    }

    public void OnUpdate()
    {
        if (_health <= 0) return;

        if (Input.GetKeyDown(KeyCode.W))
        {
            _visual.transform.position += new Vector3(0, 0, 1);
        }

        else if (Input.GetKeyDown(KeyCode.S))
        {
            _visual.transform.position += new Vector3(0, 0, -1);
        }

        else if (Input.GetKeyDown(KeyCode.A))
        {
            _visual.transform.position += new Vector3(-1, 0, 0);
        }

        else if (Input.GetKeyDown(KeyCode.D))
        {
            _visual.transform.position += new Vector3(1, 0, 0);
        }

        else if (Input.GetKeyDown(KeyCode.Space))
        {
            _visual.transform.position += new Vector3(0, 1, 0);
        }
        
        Collider[] hitColliders = Physics.OverlapSphere(_visual.transform.position, 1.0f);

        foreach (var hit in hitColliders)
        {
            if (hit.CompareTag("Enemy"))
            {
                Debug.Log($"I've been hit by: {hit.name}");
                TakeDamage(1);
                break;
            }
        }

    }

    private void CastSpell(int index)
    {
        spells[index].CastSpell(_visual.transform);
    }

    private void ConfigureUnityComponents()
    {
        _visual = new GameObject(_name);
        _visual.tag = "Player";

        _rb = _visual.AddComponent<Rigidbody>();
        _rb.isKinematic = false;
        _rb.useGravity = true;

        _boxCollider = _visual.AddComponent<BoxCollider>();
        _boxCollider.size = new Vector3(1f, 1.8f, 1f);

        ConfigureTrigger();
    }

    public void ConfigureMesh(Mesh target)
    {
        _meshFilter = _visual.AddComponent<MeshFilter>();
        _meshFilter.mesh = target;
    }

    public void AssignMaterial(Material target)
    {
        _meshRenderer = _visual.AddComponent<MeshRenderer>();
        _meshRenderer.material = target;
    }

    private void ConfigureTrigger()
    {
        _triggerCollider = _visual.AddComponent<BoxCollider>();
        _triggerCollider.isTrigger = true;
        _triggerCollider.size = new Vector3(1.5f, 1.5f, 1.5f);
    }

    public void MoveToPosition(Vector3 target)
    {
        _visual.transform.position = target;
    }
    
    private void TakeDamage(float damage)
    {
        _health -= damage;
        // Debug.Log($"HP: {_health} LEFT");
        
        if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("GAME OVER");
        Object.Destroy(_visual);
    }

}