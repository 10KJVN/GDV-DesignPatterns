using System;
using System.Collections.Generic;
using Disposables;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

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

    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private LoadingScreen _loadingScreen;
    //[SerializeField] private Player _player;

    [SerializeField] private bool _gameStarted;

    [Header("Player Config")]
    [SerializeField] private Mesh playerMesh;
    [SerializeField] private Material playerMaterial;
    private PlayerManager _playerManager;

    private EnemyManager _enemyManager;
    private List<EnemyManager> _managedEnemies;

    // A list to hold all the enemies
    private List<Enemy> _enemies = new();

    private SpellBuilder _spellBuilder = new();
    private Spell _spell;

    [Header("Enemy Config")]
    [SerializeField] private Mesh enemyMesh;
    [SerializeField] private Material enemyMaterial;

    [SerializeField] private Transform[] randomStartPositions;

    [SerializeField] private HeadsUpDisplay2 _hud;
    [SerializeField] private GameObject _canvas;
    [SerializeField] private Sprite _btnSprite;
    private string _btnText = "Fire";

    private async void Start()
    {
        try
        {
            BindObjects();
            Debug.Log("Loading...");

            using (var loadingScreenDisposable =
                   new ShowLoadingScreenDisposable(_loadingScreen))
            {
                loadingScreenDisposable.SetLoadingBarPercent(0);
                await InitializeObjects();
                loadingScreenDisposable.SetLoadingBarPercent(0.33f);
                await CreateObjects();
                loadingScreenDisposable.SetLoadingBarPercent(0.66f);
                await PrepareGame();
                loadingScreenDisposable.SetLoadingBarPercent(1f);
            }

            Debug.Log("Finished loading.");
            await BeginGame();
            OnTick(Time.deltaTime);
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
        _loadingScreen = Instantiate(_loadingScreen);
    }

    // Turning on our services e.g. persistent systems.
    private async Awaitable InitializeObjects()
    {
        var build = ScriptableObject.CreateInstance<Spell>();
        _spellBuilder
            .WithName(build.Name = "yessirski")
            .WithCost(build.Cost = 10)
            .WithDamage(build.Damage = 30)
            //.WithSpeed(build.Speed = 3.5f)
            .Build();

        _spell = build;

        _playerManager = new();

        _enemyManager = new();
        _managedEnemies = new();
        _hud = new();
    }

    // Loading in our Entities / Gameplay Objects
    private async Awaitable CreateObjects()
    {
        _background = Instantiate(_background);
        //_player = Instantiate(_player);

        _playerManager.OnStart();
        _playerManager.ConfigureMesh(playerMesh);
        _playerManager.AssignMaterial(playerMaterial);

        _enemyManager.OnStart();

        _enemyManager.ConfigureMesh(enemyMesh);
        _enemyManager.AssignMaterial(enemyMaterial);

        _managedEnemies.Add(_enemyManager);

        // Create some enemies
        for (int i = 0; i < 1; i++)
        {
            _enemies.Add(new Enemy());
        }

        foreach (Enemy enemy in _enemies)
        {
            var enemyGo = new GameObject("TestEnemy");
            enemyGo.AddComponent<Enemy>();
        }

        //TODO: Enemies

        _hud.CreateButtons(_canvas.transform, _btnText);
        _hud.AssignSprite(_btnSprite);
    }

    // Setting up our objects
    private async Awaitable PrepareGame()
    {
        // _player.MoveToPosition();
        // _player.SetStartingElement();

        // level and ui logic
        SceneManager.LoadScene("Level", LoadSceneMode.Additive);
        print(_managedEnemies.Count + " enemies");

    }

    // Here you decide the game's flow
    private async Awaitable BeginGame()
    {
        _gameStarted = true;
        // Show UI animation e.g. stage 1, stage 2 etc.
        // rest of game flow

        _spell.Cast(_mainCamera.transform);
    }

    // TODO: find a way to actually make it tick?
    private async void OnTick(float dt)
    {
        dt = Time.deltaTime;
        print(dt + " BeginGame fired the first call.");

    }
}
