using System;
using System.Collections;
using ImprovedTimers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Abilities
{
    public class AbilityExecutor : MonoBehaviour
    {
        [SerializeField] private AbilityData ability;
        [SerializeField] private GameObject target;
        
        CountdownTimer castTimer;

        private void Awake()
        {
            castTimer = new CountdownTimer(ability.castTime);
            //castTimer.OnTimerStart = () => animationController.OrNull()?.PlayOneShot(ability.animationClip);
            castTimer.OnTimerStop = SpawnVFX;
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
        }

        public void Execute(GameObject target)
        {
            castTimer.Start();
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