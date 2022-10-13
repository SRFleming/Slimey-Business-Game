using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class MapGenerator : MonoBehaviour {
    [SerializeField] private int map_width;
    [SerializeField] private int map_height;
    [SerializeField] private float scale_noise;
    [SerializeField] private float offset_x;
    [SerializeField] private float offset_y;

    [SerializeField] private GameObject ground;
    [SerializeField] private Renderer textureRenderer;
    [SerializeField] public bool autoUpdate;
    [SerializeField] private GameObject[] tree_prefabs;
    [SerializeField] private int tree_amount;
    [SerializeField] private int scatter_area_scale;
    [SerializeField] private float treeMinScale;
    [SerializeField] private float treeMaxScale;

    [Space]

    [SerializeField] private int grass_amount;
    [SerializeField] private GameObject[] grass_prefabs;
    [SerializeField] private float grassMinScale;
    [SerializeField] private float grassMaxScale;

    public void GenerateMap() {
        Texture2D per_noise = PerlinNoise.GenerateNoiseTex(map_width, map_height, scale_noise, offset_x, offset_y);
        Clear();
        scatterPrefabs(tree_prefabs, treeMinScale, treeMaxScale, tree_amount);
        scatterPrefabs(grass_prefabs, grassMinScale, grassMaxScale, grass_amount);
        //generateOuterBorder(tree_prefabs);
        textureRenderer.sharedMaterial.mainTexture = per_noise;
        textureRenderer.transform.localScale = new Vector3(per_noise.width, 1, per_noise.height);
    }
    
    public void scatterPrefabs(GameObject[] prefabs, float minScale, float maxScale, int scatter_amount) {

        for (int i = 0; i < scatter_amount; i++) {
            float sampleX = Random.Range(-map_width*scatter_area_scale, map_width*scatter_area_scale);
            float sampleY = Random.Range(-map_height*scatter_area_scale, map_height*scatter_area_scale);

            Vector3 ray = new Vector3(sampleX, 1, sampleY);

            if (!Physics.Raycast(ray, Vector3.down, out RaycastHit hit, Mathf.Infinity)) {
                continue;
            }


            int rdm = Random.Range(0, prefabs.Length);
            GameObject instantiatedPrefab = Instantiate(prefabs[rdm], hit.point, Quaternion.identity);
            instantiatedPrefab.transform.position = hit.point;
            instantiatedPrefab.transform.parent = this.transform;
            var _size = Random.Range(minScale, maxScale);
            instantiatedPrefab.transform.localScale = new Vector3(_size, _size, _size);
            instantiatedPrefab.transform.localRotation = Quaternion.Euler(new Vector3(0, Random.Range(0f, 360f), 0));
        }
    }

    /*
    public void generateOuterBorder(GameObject[] prefabs) {
        Clear();
        int rdm = Random.Range(0, prefabs.Length);
        for (int i = 0; i < 10; i++) {
            GameObject instantiatedPrefab = Instantiate(prefabs[rdm], new Vector3((-ground.transform.localScale.x * 5) * i+1, 1, -ground.transform.localScale.z * 5) , Quaternion.identity);
        }
        
    }*/

    public void Clear() {
        while (transform.childCount != 0) {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }
}
