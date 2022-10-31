using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ASpeedPowerUp : MonoBehaviour
{
    private GameObject HUD;
    private GameObject PowerupSound;
    public ParticleSystem particles;

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            HUD = GameObject.Find("HUD");
            HUD.GetComponent<UIGameStatusText>().AttackSpeedUp();
            PowerupSound = GameObject.Find("Sounds");
            PowerupSound.GetComponent<PlaySounds>().PlayPowerup();
            Pickup(col.gameObject);
        }
    }

    void Pickup(GameObject player)
    {
        player.GetComponent<PlayerController>().AddASpeedMulti(0.25f);
        ParticleSystem effect = Instantiate(particles);
        effect.transform.position = transform.position;
        Destroy(gameObject);
    }
}
