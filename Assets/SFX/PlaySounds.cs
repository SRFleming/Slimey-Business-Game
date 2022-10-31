using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySounds : MonoBehaviour
{
    public AudioSource Hover;
    public AudioSource Click;
    public AudioSource Shoot;
    public AudioSource Death;
    public AudioSource EnemyKilled;
    public AudioSource Win;
    public AudioSource Powerup;
    
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
     }
    
    public void PlayHover() {
        Hover.Play ();
    }

    public void PlayClick() {
        Click.Play ();
    }

    public void PlayDeath() {
        Death.Play ();
    }

    public void PlayEnemyKilled() {
        EnemyKilled.Play ();
    }

    public void PlayShoot() {
        Shoot.Play ();
    }

    public void PlayWin() {
        Win.Play ();
    }

    public void PlayPowerup() {
        Powerup.Play ();
    }
}
