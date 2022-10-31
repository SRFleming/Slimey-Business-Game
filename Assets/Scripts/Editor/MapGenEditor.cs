using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof (NoiseMapGen), true)]
public class NoiseMapGenEditor : Editor {
    public override void OnInspectorGUI() {
        NoiseMapGen gen_map = (NoiseMapGen)target;

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

[CustomEditor(typeof (RayMapGen), true)]
public class RayMapGenEditor : Editor {
    public override void OnInspectorGUI() {
        RayMapGen gen_map = (RayMapGen)target;

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
