using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : MonoBehaviour
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
        player.GetComponent<PlayerController>().AddDamageMulti(0.25f);
        Destroy(gameObject);
    }
}
