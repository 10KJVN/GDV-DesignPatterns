using UnityEngine;

public class OrbitalSpellStrategy : SpellStrategy
{
    public GameObject orbPrefab;
    public int numberOfOrbs = 3;
    public float radius = 3f;
    public float rotationSpeed = 0.3f;
    public float duration = 5f;
    
    public override void CastSpell(Transform origin)
    {
        Transform orbParent = CreateOrbParent(origin);
        RotateOrbParent(orbParent);
    }

    private void RotateOrbParent(Transform orbParent)
    {
        float rotationRate = 360f * rotationSpeed;
    }

    private Transform CreateOrbParent(Transform origin)
    {
        var orbParent = new GameObject("OrbParent").transform;
        orbParent.position = origin.position;
        orbParent.rotation = origin.rotation;
        orbParent.SetParent(origin);
        return orbParent;
    }
}