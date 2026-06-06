using System;
using System.Linq;
using Abilities;
using Extensions;
using UnityEngine;
using Strategies;
using UnityEngine.InputSystem;
using Object = UnityEngine.Object;

[Serializable]
public class AOETargeting : TargetingStrategy
{
    public GameObject aoePrefab;
    public float aoeRadius = 5f;
    public LayerMask groundLayerMask = 1;

    private GameObject _previewInstance;
    
    public override void Start(Ability ability, TargetingManager targetingManager)
    {
        this.ability = ability;
        this.targetingManager = targetingManager;
        isTargeting = true;
        
        targetingManager.SetCurrentStrategy(this);

        if (aoePrefab != null)
        {
            _previewInstance = Object.Instantiate(aoePrefab, Vector3.zero.Add(y:0.1f), Quaternion.identity);
        }

        if (targetingManager.input != null)
        {
            targetingManager.input.Click += OnClick;
        }
    }

    public override void Update()
    {
        if (!isTargeting || _previewInstance == null) return;

        _previewInstance.transform.position = GetMouseWorldPosition().Add(y: 0.1f);
    }

    Vector3 GetMouseWorldPosition()
    {
        if (targetingManager == null) return Vector3.zero;

        var ray = targetingManager.cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        return Physics.Raycast(ray, out var hit, 100f, groundLayerMask) ? hit.point : Vector3.zero;
    }

    public override void Cancel()
    {
        isTargeting = false;
        
        targetingManager.ClearCurrentStrategy();

        if (_previewInstance != null)
        {
            Object.Destroy(_previewInstance);
        }

        if (targetingManager.input != null)
        {
            targetingManager.input.Click -= OnClick;
        }
    }

    private void OnClick(RaycastHit hit)
    {
        if (isTargeting)
        {
            var targets = Physics.OverlapSphere(hit.point, aoeRadius)
                .Select(c => c.GetComponent<IDamagable>())
                .OfType<IDamagable>();

            foreach (var target in targets)
            {
                ability.Execute(target);
            }
            
            Cancel();
        }
    }
}