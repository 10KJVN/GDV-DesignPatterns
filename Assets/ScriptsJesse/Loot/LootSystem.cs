using System;
using UnityEngine;

public class LootSystem
{
    private ItemFactory itemFactory;
    private CurrencyFactory currencyFactory;


    // events om loot drops te communiceren naar andere systemen

    public event Action<GameObject, ItemDefinition> OnItemSpawned;

    public event Action<GameObject, CurrencyDefinition> OnCurrencySpawned;

    //constructor
    public LootSystem(ItemFactory itemFactory, CurrencyFactory currencyFactory)
    {
        this.itemFactory = itemFactory;
        this.currencyFactory = currencyFactory;
    }


    public void Subscribe(Enemy enemy)
    {
        enemy.OnDeath += () => OnEnemyDeath(enemy);
    }


    private void OnEnemyDeath(Enemy enemy)
    {
        LootResult loot = enemy.GetLoot();

        
        foreach (LootDrop drop in loot.Items)
        {
            ItemDefinition item =
                itemFactory.CreateItem(
                    drop.ItemType,
                    drop.Rarity,
                    drop.ArmorType,
                    drop.WeaponType
                );


            if (item != null)
            {
                GameObject lootObject =
                    SpawnLootPrefab(
                        item.prefab,
                        enemy.Position
                    );


                if (lootObject != null)
                {
                    OnItemSpawned?.Invoke(
                        lootObject,
                        item
                    );
                }


                Debug.Log($"Dropped: {item.itemName} " + $"({item.rarity})");
            }
        }


        foreach (CurrencyDrop drop in loot.Currency)
        {
            CurrencyDefinition currency =
                currencyFactory.CreateCurrency(
                    drop.Type
                );


            if (currency != null)
            {
                for (int i = 0; i < drop.Amount; i++)
                {
                    GameObject lootObject =
                        SpawnLootPrefab(
                            currency.prefab,
                            enemy.Position
                        );


                    if (lootObject != null)
                    {
                        OnCurrencySpawned?.Invoke(
                            lootObject,
                            currency
                        );
                    }
                }


                Debug.Log($"Dropped: {drop.Amount} {drop.Type}");
            }
        }
    }


    private GameObject SpawnLootPrefab(GameObject prefab, Vector3 position)
    {
        if (prefab == null)
        {
            Debug.LogWarning("Loot prefab is missing.");
            return null;
        }


        Vector3 spawnPosition = position + Vector3.up * 0.5f;


        GameObject lootObject =
            UnityEngine.Object.Instantiate(
                prefab,
                spawnPosition,
                UnityEngine.Random.rotation
            );


        Rigidbody rb = lootObject.GetComponent<Rigidbody>();


        if (rb != null)
        {
            Vector3 direction = UnityEngine.Random.insideUnitSphere;


            direction.y = Mathf.Abs(direction.y);

            float force = UnityEngine.Random.Range(2f, 5f);


            rb.AddForce(
                direction.normalized * force,
                ForceMode.Impulse
            );


            rb.AddTorque(
                UnityEngine.Random.insideUnitSphere * 5f,
                ForceMode.Impulse
            );
        }


        return lootObject;
    }
}
