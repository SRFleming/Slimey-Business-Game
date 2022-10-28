using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : MonoBehaviour
{
    public ParticleSystem particles;

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            Pickup(col.gameObject);
        }
    }

    void Pickup(GameObject player)
    {   
        player.GetComponent<PlayerController>().AddSpeedMulti(0.5f);
        ParticleSystem effect = Instantiate(particles);
        effect.transform.position = transform.position;
        Destroy(gameObject);
    }
}
