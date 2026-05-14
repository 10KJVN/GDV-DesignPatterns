using System;
using Disposables;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// The single entry point to our game.
/// Contains the only start method in our game
///
/// This class handles bindings, initialization and
/// the creation of objects within our scene(s).
/// </summary>

public class GameInitiator : MonoBehaviour
{
    // TODO: Serialized Ref for each class
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private Light _mainDirectionalLight;
    [SerializeField] private EventSystem _mainEventSystem;
    [SerializeField] private GameObject _background;
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private LoadingScreen _loadingScreen;
    [SerializeField] private Player _player;
    [SerializeField] private Spell _spell;

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
        //TODO: (OPTIONAL) Dependency Injection.
    }

    // Turning on our services e.g. persistent systems.
    private async Awaitable InitializeObjects()
    { }
    
    // Loading in our Entities / Gameplay Objects
    private async Awaitable CreateObjects()
    {
        _background = Instantiate(_background);
        _player = Instantiate(_player);
        _spell = Instantiate(_spell);

        //TODO: LevelUI, Obstacles
    }
    
    // Setting up our objects
    private async Awaitable PrepareGame()
    {
        // _player.MoveToPosition();
        // _player.SetStartingElement();
        
        _spell = new Spell.Builder()
            .WithName("MiBombo")
            .WithHealth(100)
            .WithSpeed(5)
            .WithDamage(10)
            .Build();
        
        // level and ui logic
    }
    
    // Here you decide the game's flow
    private async Awaitable BeginGame()
    {
        // Show UI animation e.g. stage 1, stage 2 etc.
        // rest of game flow
    }
}
