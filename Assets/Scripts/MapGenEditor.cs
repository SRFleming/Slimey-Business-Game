using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor (typeof (MapGen))]
public class MapGenEditor : Editor {
    public override void OnInspectorGUI() {
        MapGen gen_map = (MapGen)target;

        if (DrawDefaultInspector()) {
            if (gen_map.autoUpdate) {
                gen_map.Generate();
            }
        }

        if (GUILayout.Button("Generate Map")) {
            gen_map.Generate();
        }
    } 
    
}
