// COMP30019 - Graphics and Interaction
// (c) University of Melbourne, 2022

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private float nextWaveDelay;
    [SerializeField] private SpawnEnemies[] waves;
    [SerializeField] private UnityEvent<int> waveIncoming;
    [SerializeField] private UnityEvent<int> waveSpawned;
    [SerializeField] private UnityEvent<int> waveDefeated;
    
    private GameObject gameManager;
    private NextLevel nextLevel;
    private Queue<SpawnEnemies> _nextWaves;
    private SpawnEnemies _currentWave;
    public UnityEvent allWavesDefeated;

    private void Awake()
    {
        // Ensure all waves are inactive from the start. This has to be done
        // inside Awake() to avoid the Start() method being called inside the
        // individual swarm manager instances (which spawns the enemies).
        this._nextWaves = new Queue<SpawnEnemies>(this.waves);
        foreach (var wave in this._nextWaves)
            wave.gameObject.SetActive(false);
        
    }

    private void Start()
    {
        gameManager = GameObject.Find("GameManager");
        nextLevel = gameManager.GetComponent<NextLevel>();
        allWavesDefeated.AddListener(nextLevel.goToNextLevel);
        StartCoroutine(WaveSequence());
    }

    private IEnumerator WaveSequence()
    {
        var waveNumber = 0;
        
        while (this._nextWaves.Count > 0)
        {
            this._currentWave = this._nextWaves.Dequeue();
            this._currentWave.gameObject.SetActive(true);
            waveNumber += 1;
            
            this.waveIncoming.Invoke(waveNumber);

            yield return new WaitForSeconds(this.nextWaveDelay);

            // Setting current wave object to active invokes its Start() method
            // thus spawning the wave. Wait until all spawning completes.
            yield return new WaitUntil(() => this._currentWave.Spawned);
            this.waveSpawned.Invoke(waveNumber);
            
            // Wave begins to attack - player must defeat it!
            yield return new WaitUntil(() => this._currentWave.Defeated);
            this.waveDefeated.Invoke(waveNumber);
            
            yield return new WaitForSeconds(3f);
            
            // Destroy old swarm object - no need to keep it around!
            Destroy(this._currentWave.gameObject);
            
        }
        
        // Player has won - no more waves!
        print(waves);
        this.allWavesDefeated.Invoke();
    }
}
