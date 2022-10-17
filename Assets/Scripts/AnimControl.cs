using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimControl : MonoBehaviour {

    [SerializeField] private GameObject player;

    void Update() {

        if (Input.GetKeyDown(KeyCode.W)) {
            player.GetComponent<Animator>().Play("RunForward");
        } else if (Input.GetKeyDown(KeyCode.A)) {
            player.GetComponent<Animator>().Play("RunLeft");
        } else if (Input.GetKeyDown(KeyCode.D)) {
            player.GetComponent<Animator>().Play("RunRight");
        } else if (Input.GetKeyDown(KeyCode.S)) {
            player.GetComponent<Animator>().Play("RunBackward");
        } else {
            player.GetComponent<Animator>().Play("Idle");
        }
    }
}
