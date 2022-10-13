using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor (typeof (MapGenerator))]
public class MapGenEditor : Editor
{
    public override void OnInspectorGUI() {
        MapGenerator gen_map = (MapGenerator)target;

        if (DrawDefaultInspector()) {
            if (gen_map.autoUpdate) {
                gen_map.GenerateMap();
            }
        }

        if (GUILayout.Button("Generate Map")) {
            gen_map.GenerateMap();
        }
    } 
    
}
