using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameBootstrap : MonoBehaviour
{
    // enemy settings

    [Header("Starting Enemies")]

    [SerializeField]
    private EnemyDefinition[] startingEnemies;


    [SerializeField]
    private Transform[] enemySpawnPoints;


    [SerializeField]
    private float respawnDelay = 0.1f;


    // ui settings

    [Header("UI")]

    [SerializeField]
    private TMP_Text goldText;


    [SerializeField]
    private TMP_Text diamondText;


    [SerializeField]
    private GameObject itemInfoPanel;


    [SerializeField]
    private TMP_Text itemTitleText;


    [SerializeField]
    private TMP_Text itemDescriptionText;


    private LootSystem lootSystem;


    private Dictionary<GameObject, Enemy>
        activeEnemies =
        new Dictionary<GameObject, Enemy>();

    private Dictionary<GameObject, ItemDefinition>
        worldItems =
        new Dictionary<GameObject, ItemDefinition>();

    private Dictionary<GameObject, CurrencyDefinition>
        worldCurrency =
        new Dictionary<GameObject, CurrencyDefinition>();


    private int goldAmount = 0;

    private int diamondAmount = 0;


    private void Start()
    {
        CreateSystems();

        SetupLootEvents();

        SetupUI();

        SpawnStartingEnemies();
    }


    private void CreateSystems()
    {
        ItemFactory itemFactory =
            new ItemFactory(
                itemDefinitions
            );


        CurrencyFactory currencyFactory =
            new CurrencyFactory(
                currencyDefinitions
            );


        lootSystem =
            new LootSystem(
                itemFactory,
                currencyFactory
            );
    }


    private void SetupLootEvents()
    {
        lootSystem.OnItemSpawned += RegisterItem;

        lootSystem.OnCurrencySpawned += RegisterCurrency;
    }


    private void RegisterItem(
        GameObject lootObject,
        ItemDefinition item)
    {
        if (lootObject == null ||
            item == null)
        {
            return;
        }


        worldItems.Add(
            lootObject,
            item
        );
    }


    private void RegisterCurrency(
        GameObject lootObject,
        CurrencyDefinition currency)
    {
        if (lootObject == null ||
            currency == null)
        {
            return;
        }


        worldCurrency.Add(
            lootObject,
            currency
        );
    }


    private void SetupUI()
    {
        UpdateCurrencyUI();

        if (itemInfoPanel != null)
        {
            itemInfoPanel.SetActive(false);
        }
    }


    private void SpawnStartingEnemies()
    {
        int amount = Mathf.Min(startingEnemies.Length, enemySpawnPoints.Length);

        for (
            int i = 0;
            i < amount;
            i++)
        {
            SpawnEnemy(startingEnemies[i], enemySpawnPoints[i]);
        }
    }


    private Enemy SpawnEnemy(EnemyDefinition definition, Transform spawnPoint)
    {
        GameObject enemyObject =
            Instantiate(
                definition.prefab,
                spawnPoint.position,
                spawnPoint.rotation
            );


        Enemy enemy =
            new Enemy(
                definition,
                enemyObject
            );


        activeEnemies.Add(
            enemyObject,
            enemy
        );


        lootSystem.Subscribe(enemy);


        return enemy;
    }


    private void Update()
    {
        HandleEnemyInteraction();

        HandleLootInteraction();
    }


    private void HandleEnemyInteraction()
    {
        if (!Input.GetMouseButtonDown(1))
        {
            return;
        }


        List<Enemy> enemiesToKill = new List<Enemy>(activeEnemies.Values);

        foreach (Enemy enemy in enemiesToKill)
        {
            KillEnemy(enemy);
        }
    }


    private void HandleLootInteraction()
    {
        Camera camera = Camera.main;


        if (camera == null)
        {
            return;
        }


        Ray ray =
            camera.ScreenPointToRay(
                Input.mousePosition
            );


        if (!Physics.Raycast(
            ray,
            out RaycastHit hit,
            100f))
        {
            return;
        }


        GameObject hoveredObject = hit.collider.transform.root.gameObject;


        if (worldCurrency.ContainsKey(hoveredObject))
        {
            PickupCurrency(hoveredObject);
            
            return;
        }


        if (worldItems.ContainsKey(hoveredObject) && Input.GetMouseButtonDown(0))
        {
            PickupItem(hoveredObject);
        }
    }


    private void PickupCurrency(GameObject lootObject)
    {
        if (!worldCurrency.TryGetValue(
            lootObject,
            out CurrencyDefinition currency))
        {
            return;
        }


        switch (currency.currencyType)
        {
            case CurrencyType.Gold:

                goldAmount++;

                break;


            case CurrencyType.Diamond:

                diamondAmount++;

                break;
        }


        UpdateCurrencyUI();


        worldCurrency.Remove(lootObject);


        Destroy(lootObject);
    }


    private void PickupItem(GameObject lootObject)
    {
        if (!worldItems.TryGetValue(
            lootObject,
            out ItemDefinition item))
        {
            return;
        }


        ShowItemInfo(item);


        worldItems.Remove(lootObject);


        Destroy(lootObject);
    }


    private void ShowItemInfo(ItemDefinition item)
    {
        if (itemInfoPanel == null)
        {
            return;
        }


        itemInfoPanel.SetActive(true);


        itemTitleText.text = item.itemName;


        itemDescriptionText.text = item.description;
    }


    private void UpdateCurrencyUI()
    {
        if (goldText != null)
        {
            goldText.text =
                "Gold: " +
                goldAmount;
        }


        if (diamondText != null)
        {
            diamondText.text =
                "Diamonds: " +
                diamondAmount;
        }
    }


    private void KillEnemy(Enemy enemy)
    {
        GameObject enemyObject = enemy.GameObject;


        Vector3 deathPosition = enemy.Position;


        EnemyDefinition definition = enemy.Definition;


        // Loot wordt gegenereerd
        enemy.Die();


        activeEnemies.Remove(enemyObject);


        Destroy(enemyObject);


        if (definition != null)
        {
            StartCoroutine(
                RespawnEnemy(
                    definition,
                    deathPosition
                )
            );
        }
    }


    private IEnumerator RespawnEnemy(EnemyDefinition definition, Vector3 position)
    {
        yield return new WaitForSeconds(respawnDelay);


        GameObject enemyObject =
            Instantiate(
                definition.prefab,
                position,
                Quaternion.identity
            );


        Enemy enemy =
            new Enemy(
                definition,
                enemyObject
            );


        activeEnemies.Add(
            enemyObject,
            enemy
        );


        lootSystem.Subscribe(enemy);


        Debug.Log(definition.enemyName + " respawned.");
    }


    [Header("Item Definitions")]

    [SerializeField]
    private List<ItemDefinition>
        itemDefinitions;


    [Header("Currency Definitions")]

    [SerializeField]
    private List<CurrencyDefinition>
        currencyDefinitions;
}