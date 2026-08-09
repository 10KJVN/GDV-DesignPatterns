using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileSpellStrategy", menuName = "Spells/ProjectileSpawnerStrategy")]
public class ProjectileSpellStrategy : SpellStrategy
{
    public GameObject projectilePrefab;
    public float speed = 10f;
    public float duration = 10f;
    public float damage = 10f;

    public override void CastSpell(Transform origin)
    {
        CreateInstance<ProjectileBuilder>()
            .WithProjectilePrefab(projectilePrefab)
            .WithSpeed(speed)
            .WithDuration(duration)
            .WithDamage(damage)
            .Build(origin);
    }
}