using System;
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
        // _player.SetStartingElement();

        // level and ui logic
        SceneManager.LoadScene("Level", LoadSceneMode.Additive);
        print(_managedEnemies.Count + " enemies");

        _hud.OnStart();

    }

    // Here you decide the game's flow
    private async Awaitable BeginGame()
    {
        _gameStarted = true;
        // Show UI animation e.g. stage 1, stage 2 etc.
        // rest of game flow
    }

    private void OnTick()
    {
        _playerManager?.OnUpdate();
        
        foreach (var enemy in _managedEnemies)
        {
            enemy.OnUpdate();
        }
    }

    private void Update()
    {
        OnTick();
    }
}
