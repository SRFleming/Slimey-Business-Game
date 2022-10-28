using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class SpawnEnemies : MonoBehaviour {
    private int map_width = 12;
    private int map_height = 12;
    private int scatter_area_scale = 4;
    public bool autoUpdate {get; private set;} = true;

    private List<GameObject> remove_objects = new List<GameObject>();
    private List<GameObject> aliveEnemies = new List<GameObject>();


    // Find out how to pipe all of these parameters in from a different script. 
    // Should be able to call SpawnEnemyWave with all parameters whenever from gameStateManager

    // It is furthermore important that the script is put on a null with the map GameObjects as siblings to check intersections
    [SerializeField] public GameObject player;
    [SerializeField] public GameObject[] enemies;
    [SerializeField] public float minScale;
    [SerializeField] public float maxScale;
    [SerializeField] public int amount;
    private bool levelComplete;
    private int defeatCount;

    public bool Spawned { get; private set; }
    public bool Defeated { get; private set; }
    
    public void Start() {
        levelComplete = false;
        SpawnEnemyWave(enemies, amount, minScale, maxScale, player);
        Spawned = true;
    }



    public void Update(){
        this.aliveEnemies.RemoveAll(enemy => enemy == null);
        if(aliveEnemies.Count == 0){
            Defeated = true;
        }
    }

    public void SpawnEnemyWave(GameObject[] prefabs, int amount, float minScale, float maxScale, GameObject player) {
        int init_amount = amount;

        for (int i = 0; i < amount; i++) {
            float sampleX = Random.Range(-map_width*scatter_area_scale, map_width*scatter_area_scale);
            float sampleY = Random.Range(-map_height*scatter_area_scale, map_height*scatter_area_scale);

            Vector3 ray = new Vector3(sampleX, 1, sampleY);

            if (!Physics.Raycast(ray, Vector3.down, out RaycastHit hit, Mathf.Infinity)) {
                continue;
            }

            if (hit.point.x < player.transform.position.x + 5f && hit.point.x > player.transform.position.x -5f) {
                continue;
            }
            if (hit.point.z < player.transform.position.x + 5f && hit.point.z > player.transform.position.x -5f) {
                continue;
            }

            int rdm = Random.Range(0, prefabs.Length);
            GameObject instantiatedPrefab = Instantiate(prefabs[rdm], hit.point, Quaternion.identity);
            instantiatedPrefab.transform.position = hit.point;
            var _size = Random.Range(minScale, maxScale);
            instantiatedPrefab.transform.localScale = new Vector3(_size, _size, _size);
            instantiatedPrefab.transform.localRotation = Quaternion.Euler(new Vector3(0, Random.Range(0f, 360f), 0));
            instantiatedPrefab.transform.parent = this.transform;

            // Checking for intersections with any children
            if (transform.childCount > 1) {
                for (int x = 0; x < transform.childCount-1; x++) {
                    if (instantiatedPrefab.GetComponentInChildren<Collider>().bounds.Intersects(transform.GetChild(x).GetComponentInChildren<Collider>().bounds)) {
                        remove_objects.Add(instantiatedPrefab);
                        amount += 1;
                    }
                }      
            }
            
            // Checking for intersections for all siblings.
            if (transform.parent.childCount > 1 && transform.parent is not null) {
                for (int c = 0; c < transform.parent.childCount; c++) {
                    if (c != transform.GetSiblingIndex()) {
                        for (int j = 0; j < transform.parent.GetChild(c).childCount; j++) {
                            if (instantiatedPrefab.GetComponentInChildren<Collider>().bounds.Intersects(transform.parent.GetChild(c).GetChild(j).GetComponentInChildren<Collider>().bounds)) {
                                remove_objects.Add(instantiatedPrefab);
                                amount += 1;
                            }
                        }
                    }
                }
            }
            if(instantiatedPrefab){
                aliveEnemies.Add(instantiatedPrefab);
            }
        }

        amount = init_amount;

        foreach (GameObject obj in remove_objects) {
            DestroyImmediate(obj);
        }
    }
}
