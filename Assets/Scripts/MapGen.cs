using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class MapGen : MonoBehaviour {
    Dictionary<int, GameObject> tiles;
    Dictionary<int, GameObject> tile_names;
    [SerializeField] private GameObject prefab_empty;
    [SerializeField] private GameObject prefab_grass;
    [SerializeField] private GameObject prefab_smallforest;
    [SerializeField] private GameObject prefab_rocks;
    [SerializeField] private GameObject prefab_mapedges;

    [SerializeField] private int map_width;
    [SerializeField] private int map_height;

    [SerializeField] private int x_offset;
    [SerializeField] private int z_offset;
    [SerializeField] private float noise_scale;
    [SerializeField] public bool autoUpdate;
 
    List<List<int>> noise = new List<List<int>>();
    List<List<GameObject>> tile_grid = new List<List<GameObject>>();
 
    public void Generate() {
        Clear();
        CreateTiles();
        FindTileNames();
        GenerateMap();
        GenerateMapEdge();
    }
 
    public void CreateTiles() { 
        tiles = new Dictionary<int, GameObject>();
        tiles.Add(0, prefab_empty);
        tiles.Add(1, prefab_grass);
        tiles.Add(2, prefab_smallforest);
        tiles.Add(3, prefab_rocks);
    }
 
    public void FindTileNames() {
        tile_names = new Dictionary<int, GameObject>();

        foreach(KeyValuePair<int, GameObject> prefab_pair in tiles) {
            GameObject tile_name = new GameObject(prefab_pair.Value.name);
            tile_name.transform.parent = gameObject.transform;
            tile_name.transform.localPosition = new Vector3(0, 0, 0);
            tile_names.Add(prefab_pair.Key, tile_name);
        }
    }
 
    public void GenerateMap() {
        for (int x = 0; x < map_width; x++) {
            noise.Add(new List<int>());
            tile_grid.Add(new List<GameObject>());
            for (int z = 0; z < map_height; z++) {
                int id = CalcPerlinValue(x, z);
                noise[x].Add(id);
                InstantiateTile(id, x, z);
            }
        }
    }
 
    public int CalcPerlinValue(int x, int z) {
        float perlin_value = Mathf.PerlinNoise((x - x_offset) / noise_scale, (z - z_offset) / noise_scale);
        float clamped_perlin_val = Mathf.Clamp(perlin_value, 0.0f, 1.0f);
        float perlin_scaled = tiles.Count * clamped_perlin_val;
 
        if(perlin_scaled == tiles.Count) {
            perlin_scaled = (tiles.Count - 1);
        }
        return Mathf.FloorToInt(perlin_scaled);
    }

    public void InstantiateTile(int tile_id, int x, int z) {
        float scaled_x = x * 10 - (map_width * 10) / 2;
        float scaled_y = z * 10 - (map_height * 10) / 2;

        GameObject tile_prefab = tiles[tile_id];
        GameObject tile_name = tile_names[tile_id];
        GameObject tile = Instantiate(tile_prefab, tile_name.transform);
 
        tile.transform.localPosition = new Vector3(scaled_x, 0, scaled_y);

        int[] possible_rotations = new int[] {90, 180, 270};
        int rdm = Random.Range(0, possible_rotations.Length);
        tile.transform.localRotation = Quaternion.Euler(new Vector3(0, possible_rotations[rdm], 0));
 
        tile_grid[x].Add(tile);
    }

    public void GenerateMapEdge() {

    }

    public void Clear() {
        while (transform.childCount != 0) {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }
}

