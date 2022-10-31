using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : MonoBehaviour
{
    private GameObject HUD;
    private GameObject PowerupSound;
    public ParticleSystem particles;

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            HUD = GameObject.Find("HUD");
            HUD.GetComponent<UIGameStatusText>().SpeedUp();
            PowerupSound = GameObject.Find("Sounds");
            PowerupSound.GetComponent<PlaySounds>().PlayPowerup();
            Pickup(col.gameObject);
        }
    }

    void Pickup(GameObject player)
    {   
        player.GetComponent<PlayerController>().AddSpeedMulti(0.35f);
        ParticleSystem effect = Instantiate(particles);
        effect.transform.position = transform.position;
        Destroy(gameObject);
    }
}
