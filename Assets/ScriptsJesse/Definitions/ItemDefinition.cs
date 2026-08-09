using UnityEngine;

[CreateAssetMenu(menuName = "Loot/Item Definition")]
public class ItemDefinition : ScriptableObject
{
    [Header("Basic Info")]
    public string itemName;

    [TextArea]
    public string description;

    [Header("Classification")]
    public ItemType itemType;

    public ArmorType armorType;

    public WeaponType weaponType;

    public Rarity rarity;

    [Header("Prefab")]
    public GameObject prefab;
}