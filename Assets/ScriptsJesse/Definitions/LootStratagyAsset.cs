using UnityEngine;

public abstract class LootStrategyAsset: ScriptableObject, ILoot
{
    public abstract LootResult GenerateLoot();
}
