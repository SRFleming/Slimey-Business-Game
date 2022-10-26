using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class RayMapGen : MonoBehaviour {
    private int map_width = 12;
    private int map_height = 12;
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private int amount;
    [SerializeField] private float minScale;
    [SerializeField] private float maxScale;
    public bool autoUpdate {get; private set;} = true;

    private List<GameObject> remove_objects = new List<GameObject>();
    private int scatter_area_scale = 4;

    void Start() {
        GenerateMap();
    }

    public void GenerateMap() {
        Clear();
        scatterPrefabs(prefabs, amount, minScale, maxScale);
    }
    
    // Add all SerlializedFields as parameters
    public void scatterPrefabs(GameObject[] prefabs, int amount, float minScale, float maxScale) {
        int init_amount = amount;

        for (int i = 0; i < amount; i++) {
            float sampleX = Random.Range(-map_width*scatter_area_scale, map_width*scatter_area_scale);
            float sampleY = Random.Range(-map_height*scatter_area_scale, map_height*scatter_area_scale);

            Vector3 ray = new Vector3(sampleX, 1, sampleY);

            if (!Physics.Raycast(ray, Vector3.down, out RaycastHit hit, Mathf.Infinity)) {
                continue;
            }

            if (hit.point.x < 2f && hit.point.x > -2f) {
                continue;
            }
            if (hit.point.z < 2f && hit.point.z > -2f) {
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
            
            // IMPORTANT: CHECK IF PARENT EXISTS - IF YES DO THIS CODE IF NOT DONT CHECK ALL SIBLINGS.
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
        }

        amount = init_amount;

        foreach (GameObject obj in remove_objects) {
            DestroyImmediate(obj);
        }
    }

    public void Clear() {
        while (transform.childCount != 0) {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }
}
