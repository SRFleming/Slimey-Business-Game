// COMP30019 - Graphics and Interaction
// (c) University of Melbourne, 2022

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SwarmManager : MonoBehaviour
{
    // External parameters/variables
    [SerializeField] private BasicEnemy basicEnemyTemplate;
    [SerializeField] private ShootEnemy shootEnemyTemplate;
    [SerializeField] private SwoopEnemy swoopEnemyTemplate;
    [SerializeField] private int numBasicEnemys;
    [SerializeField] private int numShootEnemys;
    [SerializeField] private int numLungeEnemys;
    [SerializeField] private int difficultyMultiplier;

    private readonly List<BasicEnemy> _basicEnemies = new();
    private readonly List<ShootEnemy> _shootEnemies = new();
    private readonly List<SwoopEnemy> _swoopEnemies = new();
    
    public bool Spawned { get; private set; }
    public bool Defeated { get; private set; }

    private void Start()
    {

        // Use a coroutine to define the swarm attack sequence. Here we are
        // nesting coroutines to define a "higher level" sequence, which could
        // easily be built upon to create interesting variations in gameplay.
        //StartCoroutine(AttackSequence());
    }

    private bool SomeEnemiesAlive()
    {
        // Check if any enemies still exist (objects not destroyed)
        if (this._basicEnemies.Any(enemy => enemy != null))
        return true;
        else {
            return false;
        }
    }

    // Automatically generate swarm of enemies based on the given serialized
    // attributes/parameters. This has been modified to be a coroutine that
    // spawns enemies in quick succession (creates an "entrance sequence").
    private IEnumerator GenerateSwarm()
    {
        {
            var spawnPosition = transform.position;
            
            var enemy = Instantiate(
                this.basicEnemyTemplate, spawnPosition,
                this.basicEnemyTemplate.transform.rotation); // Use prefab rotation
            this._basicEnemies.Add(enemy);

            //var enemySlot = Instantiate(this.enemySlotTemplate);
            //enemySlot.SetSwarm(this, offset);
            //enemySlot.SetEnemy(enemy.GetComponent<Rigidbody>());

            // A short delay between spawning each enemy allows us to create a
            // sequential "fly in" of enemies to their slot positions. 
            yield return new WaitForSeconds(0.03f);
        }

        // Final delay to allow stabilisation of spawning.
        yield return new WaitForSeconds(1.0f);
        Spawned = true;
        
        // Let all enemies know they can begin attacking.
        /*foreach (var enemy in this._enemies)
            enemy.SwarmReady = true; */
    }

}
