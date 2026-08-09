public class LootDrop
{
    public ItemType ItemType;
    public Rarity Rarity;

    public ArmorType ArmorType;
    public WeaponType WeaponType;

    public LootDrop(
        ItemType itemType,
        Rarity rarity,
        ArmorType armorType = ArmorType.None,
        WeaponType weaponType = WeaponType.None)
    {
        ItemType = itemType;
        Rarity = rarity;

        ArmorType = armorType;
        WeaponType = weaponType;
    }
}
