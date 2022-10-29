using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupColor : MonoBehaviour
{

    void Start()
    {
        if(gameObject.name == "Damage"){
            GetComponent<MeshRenderer>().material.SetColor("_PowerColor", new Color(0.65f, 0.06f, 0.03f, 1f));
        }
        if(gameObject.name == "Speed"){
            GetComponent<MeshRenderer>().material.SetColor("_PowerColor", new Color(0.38f, 0.93f, 0.85f, 1f));
        }
        if(gameObject.name == "ASpeed"){
            GetComponent<MeshRenderer>().material.SetColor("_PowerColor", new Color(0.83f, 0.72f, 0.05f, 1f));
        }
    }

}
