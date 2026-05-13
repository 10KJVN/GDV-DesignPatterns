using System;
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
    [SerializeField] private Player _player;

    private async void Start()
    {
        try 
        {
            BindObjects();
            Debug.Log("Loading...");
            // Show loading screen

            await InitializeObjects();
            await CreateObjects();
            PrepareGame();

            // Hide loading screen
            Debug.Log("Finished loading.");
            await BeginGame();
        }
        
        catch { Debug.Log("Failed loading."); }
        
        finally { Debug.Log("Game Launched successfully."); }
    }

    // Connecting our instances
    private void BindObjects()
    {
        _mainCamera = Instantiate(_mainCamera);
        _mainDirectionalLight = Instantiate(_mainDirectionalLight);
        _mainEventSystem = Instantiate(_mainEventSystem);
        
        //TODO: loadingScreen, Spawner, lvlManager
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
        
        //TODO: LevelUI, Obstacles
    }
    
    // Setting up our objects
    private void PrepareGame()
    {
        // _player.MoveToPosition();
        // _player.SetStartingElement();
        
        // enemy or obstacle logic
        
        // level and ui logic
    }
    
    // Here you decide the game's flow
    private async Awaitable BeginGame()
    {
        // Show UI animation e.g. stage 1, stage 2 etc.
        // rest of game flow
    }
}
