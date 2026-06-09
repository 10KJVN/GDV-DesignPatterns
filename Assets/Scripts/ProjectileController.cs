using Abilities;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    Ability ability;
    private float _speed;

    public void Initialize(Ability ability, float speed)
    {
        this.ability = ability;
        _speed = speed;
        Destroy(gameObject, 5f);
    }

    private void Update() => transform.Translate(Vector3.forward * (_speed * Time.deltaTime));

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) return;

        if (other.gameObject.TryGetComponent<IDamagable>(out var target))
        {
            ability.Execute(target);
            Destroy(gameObject);
        }
    }

}