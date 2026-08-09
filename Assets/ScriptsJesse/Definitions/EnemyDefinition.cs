using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyDefinition", menuName = "Loot System/Enemy Definition")]
public class EnemyDefinition : ScriptableObject
{
    [Header("Enemy")]
    public string enemyName;

    [Header("Prefab")]
    public GameObject prefab;

    [Header("Loot")]
    public LootStrategyAsset lootStrategy;
}