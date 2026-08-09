using UnityEngine;

[CreateAssetMenu(fileName = "CommonEnemyLoot", menuName = "Loot System/Loot Strategies/Common Enemy")]
public class CommonEnemyLoot : LootStrategyAsset
{
    public override LootResult GenerateLoot()
    {
        LootResult result = new LootResult();

        // bepaal hoeveel goud er wordt gedropt

        int goldAmount = Random.Range(5, 11);

        result.Currency.Add(
            new CurrencyDrop(
                CurrencyType.Gold,
                goldAmount
            )
        );


        // bepaal of en hoeveel diamanten er worden gedropt

        int diamondAmount = Random.Range(0, 3);

        if (diamondAmount > 0)
        {
            result.Currency.Add(
                new CurrencyDrop(
                    CurrencyType.Diamond,
                    diamondAmount
                )
            );
        }


        // 50% kans op een item
        float itemChance = Random.Range(0f, 100f);

        if (itemChance <= 50f)
        {
            ItemType randomItemType = GetRandomItemType();

            result.Items.Add(
                new LootDrop(
                    randomItemType,
                    GetItemRarity()
                )
            );
        }


        return result;
    }


    private ItemType GetRandomItemType()
    {
        int random = Random.Range(0, 2);

        if (random == 0)
        {
            return ItemType.Armor;
        }else
        {
            return ItemType.Weapon;
        }
    }


    // bepaalt wat voor rarity het item heeft

    private Rarity GetItemRarity()
    {
        float random = Random.Range(1f, 101f);

        if (random <= 1f)
        {
            return Rarity.Legendary;
        }
        else if (random <= 5f)
        {
            return Rarity.Epic;
        }
        else if (random <= 20f)
        {
            return Rarity.Rare;
        }
        else
        {
            return Rarity.Common;
        }
    }
}