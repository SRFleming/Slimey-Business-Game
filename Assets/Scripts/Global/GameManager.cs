using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SceneManager))]
public class GameManager : MonoBehaviour
{
    public const string Tag = "GameManager";
    
    public const string MenuSceneName = "MenuScene";
    public const string GameSceneName = "GameScene";
    
    private int _stats;

    //public UnityEvent<int> OnScoreChanged { get; } = new();
    /*public int Stats
    {
        get => this._stats;
        set
        {
            this._stats = value;
            OnScoreChanged.Invoke(this._stats);
        }
    } */

    private void Awake()
    {
        // Should not be created if there's already a manager present (at least
        // two total, including ourselves). This allows us to place a game
        // manager in every scene, in case we want to open scenes direct.
        if (GameObject.FindGameObjectsWithTag(Tag).Length > 1)
            Destroy(gameObject);

        // Make this game object persistent even between scene changes.
        DontDestroyOnLoad(gameObject);
        
        // Hook into scene loaded events.
        SceneManager.sceneLoaded += OnSceneLoaded;

        // Init global game state values and/or set defaults.
    }
    
    public IEnumerator GotoScene(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        
        var asyncLoadOp = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoadOp.isDone)
        {
            yield return null;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == GameSceneName)
        {
            
        }
    }
}
