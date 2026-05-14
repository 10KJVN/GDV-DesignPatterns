using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileSpellStrategy", menuName = "Spells/ProjectileSpawnerStrategy")]
public class ProjectileSpellStrategy : SpellStrategy
{
    public GameObject projectilePrefab;
    public float speed = 10f;
    public float duration = 10f;

    public override void CastSpell(Transform origin)
    {
        new ProjectileBuilder()
            .WithProjectilePrefab(projectilePrefab)
            .WithSpeed(speed)
            .WithDuration(duration)
            .Build(origin);
    }
}