using UnityEngine;
using System;

namespace Abilities
{
    public class ProjectileMover : MonoBehaviour
    {
        private Action<Collision> _onHitCallback;

        public void SetCallback(Action<Collision> callback)
        {
            _onHitCallback = callback;
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log($"[VFX] Projectile hit: {collision.gameObject.name}");
            _onHitCallback?.Invoke(collision);
        }
    }
}