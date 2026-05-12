using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// The entry point to our game.
/// Contains the only start method in our game
///
/// This class handles bindings, initialization and
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

    // Bindings

    // private async Awaitable Start()
    // {
    //     Debug.Log("Loading...");
    //     //BindObjects()
    //     // await InitializeObjects()
    //
    //     await Awaitable.WaitForSecondsAsync(2);
    //     
    //     Debug.Log("Finished loading.");
    // }

    private async void Start()
    {
        BindObjects();
        Debug.Log("Loading...");
        // _loadingScreen.Show();
        await InitializeObjects(); // init any service that needs to be set up at start of game
        // e.g. analyticsService or Unity's new input system.
        await CreateObjects();
        
        PrepareGame();
        // _loadingScreen.Hide();
        Debug.Log("Finished loading.");
        await BeginGame();
    }

    // Creates an instance of each class in the scene
    private void BindObjects()
    {
        Instantiate(_mainCamera);
        Instantiate(_mainDirectionalLight);
        // loadingScreen
        Instantiate(_mainEventSystem);
        Instantiate(_background);
        // Instantiate(_levelManager);
        Instantiate(_player);
    }

    private async Awaitable InitializeObjects()
    {
        
    }
    
    // Loading our heavy objects
    private async Awaitable CreateObjects()
    {
        // Could be through Resources
        // AssetBundle
        // Adressables
        _background = Instantiate(_background);
        _player = Instantiate(_player);
    }

    private async Awaitable DoSomething()
    {
        
    }
    
    private void PrepareGame()
    {
        // _player.MoveToPosition();
        // _player.SetStartingElement();
        
        // enemy or obstacle logic
        
        // level and ui logic
    }
    
    private async Awaitable BeginGame()
    {
        // Show UI animation e.g. stage 1, stage 2 etc.
        // await _levelUI.ShowLevelAnimation();
        
        // enable enemies
    }
}
