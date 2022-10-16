using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASpeedPowerUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            Pickup(col.gameObject);
        }
    }

    void Pickup(GameObject player)
    {
        player.GetComponent<PlayerController>().AddASpeedMulti(0.25f);
        Destroy(gameObject);
    }
}
