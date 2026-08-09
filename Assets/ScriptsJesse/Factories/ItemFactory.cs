using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemFactory
{
    private List<ItemDefinition> items;

    public ItemFactory(List<ItemDefinition> items)
    {
        this.items = items;
    }

    public ItemDefinition CreateItem(
        ItemType itemType,
        Rarity requestedRarity,
        ArmorType armorType = ArmorType.None,
        WeaponType weaponType = WeaponType.None)
    {
        // Begin bij de gevraagde rarity en ga steeds één rarity naar beneden stel dat er niet een item is van die rarity snapie
        for (
            int rarityValue = (int)requestedRarity;
            rarityValue >= (int)Rarity.Common;
            rarityValue--)
        {
            Rarity currentRarity =
                (Rarity)rarityValue;

            List<ItemDefinition> possibleItems =
                items
                .Where(item =>
                    item.itemType == itemType &&
                    item.rarity == currentRarity &&
                    MatchesSubtype(
                        item,
                        armorType,
                        weaponType))
                .ToList();

            if (possibleItems.Count > 0)
            {
                if (currentRarity != requestedRarity)
                {
                    Debug.Log($"No {requestedRarity} " + $"{itemType} found. " + $"Falling back to {currentRarity}.");
                }

                // Kies random items uit de mogelijke items.
                int randomIndex = Random.Range(0, possibleItems.Count);

                return possibleItems[randomIndex];
            }
        }

        Debug.LogWarning($"No suitable item found for " + $"{requestedRarity} {itemType}.");

        return null;
    }

    private bool MatchesSubtype(
        ItemDefinition item,
        ArmorType armorType,
        WeaponType weaponType)
    {
        // Als armorType is opgegeven moet het item die armorType hebben.
        if (armorType != ArmorType. None)
        {
            return item.armorType == armorType;
        }

        // Als weaponType is opgegevenmoet het item die weaponType hebben.
        if (weaponType != WeaponType.None)
        {
            return item.weaponType == weaponType;
        }

        // Geen subtype gevraagd.
        return true;
    }
}