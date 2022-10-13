using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePrefabs {
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private int scatter_amount;
    [SerializeField] private int scatter_area_scale;
    [SerializeField] private float minScale;
    [SerializeField] private float maxScale;

    /*public static void scatterPrefabs() {

        for (int i = 0; i < scatter_amount; i++) {
            float sampleX = Random.Range(-map_width*scatter_area_scale, map_width*scatter_area_scale);
            float sampleY = Random.Range(-map_height*scatter_area_scale, map_height*scatter_area_scale);

            Vector3 ray = new Vector3(sampleX, 1, sampleY);

            if (!Physics.Raycast(ray, Vector3.down, out RaycastHit hit, Mathf.Infinity)) {
                continue;
            }

            if (hit.point.y > 1.1) {
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
    }*/
}
