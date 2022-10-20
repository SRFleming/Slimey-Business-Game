using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    [SerializeField] public Transform player;

    public void Update() {
        this.transform.position = player.transform.position + new Vector3(0, 25f, -7.5f);
    }
}
