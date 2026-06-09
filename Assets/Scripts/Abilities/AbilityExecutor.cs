using System;
using System.Collections;
using ImprovedTimers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Abilities
{
    /// <summary>
    /// Test script, executes a SINGLE ability
    /// on a MANUALLY set target, both in the inspector.
    /// </summary>
    public class AbilityExecutor : MonoBehaviour
    {
        [SerializeField] private AbilityData ability;
        [SerializeField] private GameObject target;

        private CountdownTimer _castTimer;

        private void Awake()
        {
            _castTimer = new CountdownTimer(ability.castTime);
            //castTimer.OnTimerStart = () => animationController.OrNull()?.PlayOneShot(ability.animationClip);
            _castTimer.OnTimerStop = SpawnVFX;
        }

        private void SpawnVFX()
        {
            if (ability.vfxPrefab == null) return;
            
            var vfx = Instantiate(ability.vfxPrefab, transform.position, transform.rotation);
            vfx?.SetCallback((Collision co) =>
            {
                Debug.Log($"{ability.name}'s effects are being executed on {target.name}!");
                foreach (var effect in ability.effects)
                {
                    effect.Execute(gameObject, target);
                }
            });
            Destroy(vfx?.gameObject, 5f);
        }

        public void Execute(GameObject target)
        {
            _castTimer.Start();
        }

        private void Update()
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                Execute(target);
            }
        }
    }
}