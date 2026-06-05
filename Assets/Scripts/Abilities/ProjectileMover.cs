using UnityEngine;
using System;

namespace Abilities
{
    public class ProjectileMover : MonoBehaviour
    {
        private Action<Collision> onHitCallback;

        public void SetCallback(Action<Collision> callback)
        {
            onHitCallback = callback;
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log($"[VFX] Projectile hit: {collision.gameObject.name}");
            onHitCallback?.Invoke(collision);
            Destroy(gameObject); // MB specific method
        }
    }
}