using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    [SerializeField] public Transform player;

    public void Update() {
        this.transform.position = player.transform.position + new Vector3(0, 20f, -15.5f);
        this.transform.Rotate(new Vector3(player.transform.eulerAngles.x, 0, player.transform.eulerAngles.z));

    }
}
