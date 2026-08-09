using UnityEngine;

[CreateAssetMenu(fileName = "BossEnemyLoot", menuName = "Loot System/Loot Strategies/Boss Enemy")]
public class BossEnemyLoot : LootStrategyAsset
{
    public override LootResult GenerateLoot()
    {
        LootResult result = new LootResult();


        // bepaal hoeveel goud er wordt gedropt

        int goldAmount = Random.Range(10, 16);

        result.Currency.Add(
            new CurrencyDrop(
                CurrencyType.Gold,
                goldAmount
            )
        );


        // bepaal of en hoeveel diamanten er worden gedropt

        int diamondAmount = Random.Range(0, 5);

        if (diamondAmount > 0)
        {
            result.Currency.Add(
                new CurrencyDrop(
                    CurrencyType.Diamond,
                    diamondAmount
                )
            );
        }


        // 50% kans op 2 items.
        // Anders 1 item.

        float itemChance = Random.Range(0f, 100f);

        if (itemChance <= 50f)
        {
            result.Items.Add(
                CreateRandomItem()
            );

            result.Items.Add(
                CreateRandomItem()
            );
        }
        else
        {
            result.Items.Add(
                CreateRandomItem()
            );
        }


        return result;
    }


    private LootDrop CreateRandomItem()
    {
        ItemType randomItemType =
            GetRandomItemType();

        Rarity randomRarity =
            GetItemRarity();

        return new LootDrop(
            randomItemType,
            randomRarity
        );
    }


    private ItemType GetRandomItemType()
    {
        int random = Random.Range(0, 2);

        if (random == 0)
        {
            return ItemType.Armor;
        }

        return ItemType.Weapon;
    }


    // bepaalt wat voor rarity het item heeft

    private Rarity GetItemRarity()
    {
        float random = Random.Range(1f, 101f);

        if (random <= 5f)
        {
            return Rarity.Legendary;
        }
        else if (random <= 15f)
        {
            return Rarity.Epic;
        }
        else if (random <= 30f)
        {
            return Rarity.Rare;
        }
        else
        {
            return Rarity.Common;
        }
    }
}