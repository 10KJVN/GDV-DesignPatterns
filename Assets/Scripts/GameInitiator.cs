using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// The single entry point to our game.
/// Contains the only start method in our game
///
/// This class handles bindings, initialization and
/// the creation of objects within our scene(s).
/// </summary>

public class GameInitiator : MonoBehaviour
{
    // Prefab references
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private Light _mainDirectionalLight;
    [SerializeField] private EventSystem _mainEventSystem;
    [SerializeField] private GameObject _background;
    [SerializeField] private bool _gameStarted;

    [Header("Player Config")]
    [SerializeField] private Mesh playerMesh;
    [SerializeField] private Material playerMaterial;
    [SerializeField] private SpellStrategy[] spells;
    private PlayerManager _playerManager;
    
    private EnemyManager _enemyManager;
    private List<EnemyManager> _managedEnemies;

    [Header("Enemy Config")]
    [SerializeField] private Mesh enemyMesh;
    [SerializeField] private Material enemyMaterial;
    [SerializeField] private Mesh em2Mesh;
    [SerializeField] private Material em2Material;
    [SerializeField] private Vector3[] startPositions;
    
    [Header("External config")] 
    [SerializeField] private EnemyDefinition[] startingEnemies;
    [SerializeField] private Transform[] enemySpawnPoints;
    [SerializeField] private float respawnDelay = 0.1f;

    [Header ("UI")]
    private HeadsUpDisplay2 _hud;
    [SerializeField] private GameObject _canvas;
    [SerializeField] private Sprite _btnSprite;
    private string _btnText = "Fire";
    
    [Header("External UI")]
    [SerializeField] private TMP_Text goldText;
    [SerializeField] private TMP_Text diamondText;
    [SerializeField] private GameObject itemInfoPanel;
    [SerializeField] private TMP_Text itemTitleText;
    [SerializeField] private TMP_Text itemDescriptionText;
    
    private LootSystem _lootSystem;
    private Dictionary<GameObject, Enemy> _activeEnemies;
    private Dictionary<GameObject, ItemDefinition> _worldItems;
    private Dictionary<GameObject, CurrencyDefinition> _worldCurrency;
    private int _goldAmount;
    private int _diamondAmount;
    
    [Header("Item Definitions")]
    [SerializeField] private List<ItemDefinition> itemDefinitions;
    [Header("Currency Definitions")] 
    [SerializeField] private List<CurrencyDefinition> currencyDefinitions;

    private async void Start()
    {
        try
        {
            BindObjects();
            Debug.Log("Loading...");
            
            await InitializeObjects();
            await CreateObjects();
            await PrepareGame();
            
            Debug.Log("Finished loading.");
            await BeginGame();
            
            Debug.Log("Ticking has begun!");
        }

        catch (Exception e) { Debug.Log($"Failed loading: {e}"); }

        finally { Debug.Log("Game Launched successfully."); }
    }

    // Connecting our instances
    private void BindObjects()
    {
        _mainCamera = Instantiate(_mainCamera);
        _mainDirectionalLight = Instantiate(_mainDirectionalLight);
        _mainEventSystem = Instantiate(_mainEventSystem);

        //TODO: Spawner, lvlManager
        _canvas = Instantiate(_canvas);
    }

    // Turning on our services e.g. persistent systems.
    private async Awaitable InitializeObjects()
    {
        _playerManager = new(spells);
        _managedEnemies = new();
        _hud = new();
        
        _activeEnemies = new();
        _worldItems = new();
        _worldCurrency = new();

        _goldAmount = 0;
        _diamondAmount = 0;
        
        CreateSystems();
        SetupLootEvents();
        SetupUI();
    }

    // Loading in our Entities / Gameplay Objects
    private async Awaitable CreateObjects()
    {
        _background = Instantiate(_background);

        _playerManager.OnStart();
        _playerManager.ConfigureMesh(playerMesh);
        _playerManager.AssignMaterial(playerMaterial);

        for (int i = 0; i < 3; i++)
        {
            var enemy = new EnemyManager();
            
            enemy.OnStart();
            enemy.ConfigureMesh(enemyMesh);
            enemy.AssignMaterial(em2Material);
            enemy.MoveToPosition(startPositions[i]);
            
            _managedEnemies.Add(enemy);
        }

        _hud.CreateButtons(_canvas.transform, _btnText);
        _hud.AssignSprite(_btnSprite);
        _hud.ConfigureButton();
    }

    // Setting up our objects
    private async Awaitable PrepareGame()
    {
        var playerStart = new Vector3(0f, -1f, -10f);
        
        _playerManager.MoveToPosition(playerStart);
        
        SceneManager.LoadScene("Level", LoadSceneMode.Additive);
        print(_managedEnemies.Count + " enemies");

        SpawnStartingEnemies();
        _hud.OnStart();

    }

    // Here you decide the game's flow
    private async Awaitable BeginGame()
    {
        _gameStarted = true;
    }

    // The main game's update loop
    private void Update()
    {
        OnTick();
        
        HandleEnemyInteraction();

        HandleLootInteraction();
    }

    private void OnTick()
    {
        _playerManager?.OnUpdate();
        
        foreach (var enemy in _managedEnemies)
        {
            enemy.OnUpdate();
        }
    }
    
    #region Jesse's Code

    private void CreateSystems()
    {
        ItemFactory itemFactory = new ItemFactory(itemDefinitions);
        CurrencyFactory currencyFactory = new CurrencyFactory(currencyDefinitions);

        _lootSystem = new LootSystem(itemFactory, currencyFactory);
    }
    
    private void SetupLootEvents()
    {
        _lootSystem.OnItemSpawned += RegisterItem;

        _lootSystem.OnCurrencySpawned += RegisterCurrency;
    }
    
    private void RegisterItem(GameObject lootObject, ItemDefinition item)
    {
        if (lootObject == null || item == null)
        {
            return;
        }
        
        _worldItems.Add(lootObject, item);
    }
    
    private void RegisterCurrency(GameObject lootObject, CurrencyDefinition currency)
    {
        if (lootObject == null || currency == null)
        {
            return;
        }
        
        _worldCurrency.Add(lootObject, currency);
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

        for (int i = 0; i < amount; i++)
        {
            SpawnEnemy(startingEnemies[i], enemySpawnPoints[i]);
        }
    }
    
    private Enemy SpawnEnemy(EnemyDefinition definition, Transform spawnPoint)
    {
        var enemyObject = Instantiate(definition.prefab, spawnPoint.position, spawnPoint.rotation);
        
        Enemy enemy = new Enemy(definition, enemyObject);
        _activeEnemies.Add(enemyObject, enemy);
        
        _lootSystem.Subscribe(enemy);
        
        return enemy;
    }

    // IMPORTANT INTERACTIOIN 1!! 
    private void HandleEnemyInteraction()
    {
        if (!Input.GetMouseButtonDown(1))
        {
            return;
        }

        var enemiesToKill = new List<Enemy>(_activeEnemies.Values);

        foreach (Enemy enemy in enemiesToKill)
        {
            KillEnemy(enemy);
        }
    }
    
    private void HandleLootInteraction()
    {
        var cam = _mainCamera;
        if (cam == null)
        {
            return;
        }

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            return;
        }
        
        GameObject hoveredObject = hit.collider.transform.root.gameObject;
        
        if (_worldCurrency.ContainsKey(hoveredObject))
        {
            PickupCurrency(hoveredObject);
            
            return;
        }
        
        if (_worldItems.ContainsKey(hoveredObject) && Input.GetMouseButtonDown(0))
        {
            PickupItem(hoveredObject);
        }
    }

    private void PickupCurrency(GameObject lootObject)
    {
        if (!_worldCurrency.TryGetValue(lootObject, out CurrencyDefinition currency))
        {
            return;
        }
        
        switch (currency.currencyType)
        {
            case CurrencyType.Gold:
                _goldAmount++;
                break;
            
            case CurrencyType.Diamond:
                _diamondAmount++;
                break;
        }

        UpdateCurrencyUI();
        
        _worldCurrency.Remove(lootObject);
        Destroy(lootObject);
    }
    
    private void PickupItem(GameObject lootObject)
    {
        if (!_worldItems.TryGetValue(lootObject, out ItemDefinition item))
        {
            return;
        }

        ShowItemInfo(item);
        
        _worldItems.Remove(lootObject);
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
            goldText.text = "Gold: " + _goldAmount;
        }
        
        if (diamondText != null)
        {
            diamondText.text = "Diamonds: " + _diamondAmount;
        }
    }

    private void KillEnemy(Enemy enemy)
    {
        GameObject enemyObject = enemy.GameObject;
        Vector3 deathPosition = enemy.Position;
        EnemyDefinition definition = enemy.Definition;
        
        // Loot wordt gegenereerd
        enemy.Die();
        
        _activeEnemies.Remove(enemyObject);
        Destroy(enemyObject);

        if (definition != null)
        {
            StartCoroutine(RespawnEnemy(definition, deathPosition));
        }
    }
    
    private IEnumerator RespawnEnemy(EnemyDefinition definition, Vector3 position)
    {
        yield return new WaitForSeconds(respawnDelay);
        
        GameObject enemyObject = Instantiate(definition.prefab, position, Quaternion.identity);

        Enemy enemy = new Enemy(definition, enemyObject);

        _activeEnemies.Add(enemyObject, enemy);
        _lootSystem.Subscribe(enemy);

        Debug.Log(definition.enemyName + " respawned.");
    }
    
    #endregion
    
}
