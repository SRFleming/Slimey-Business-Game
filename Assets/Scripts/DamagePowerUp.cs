using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePowerUp : MonoBehaviour
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
        player.GetComponent<PlayerController>().AddDamageMulti(0.25f);
        Instantiate(particles);
        particles.transform.position = transform.position;
        Destroy(gameObject);
    }
}
