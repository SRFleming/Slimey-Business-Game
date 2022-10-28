using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class SwoopEnemy : MonoBehaviour
{
    [SerializeField] private float minWaitTime;
    [SerializeField] private float maxWaitTime;
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private float damageCooldown;
    [SerializeField] private string damageTag;

    private PlayerController player;
    private CharacterController cC;
    private EnemyState state;
    private Vector3 swoopDirection;
    private float currentCooldown;

    private enum EnemyState {
        Idle,
        Swoop
    }

    private void Awake()
    {
        this.player = FindObjectOfType<PlayerController>();
        this.cC = GetComponent<CharacterController>();
        this.currentCooldown = damageCooldown;
        StartCoroutine(AttackPattern());
    }

    private void Update()
    {
        if (player == null) {
            return;
        }
        if (this.state == EnemyState.Idle)
        {
            cC.Move(Vector3.zero);
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            Vector3 swoopDistance = player.transform.position - this.transform.position;
            this.swoopDirection = swoopDistance.normalized;
            float swoopAngle = Mathf.Atan2(swoopDirection.z, swoopDirection.x)*Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Euler(0,-swoopAngle,0);
        }
        else if (this.state == EnemyState.Swoop && this.player)
        {
            cC.Move(new Vector3(swoopDirection.x, 0, swoopDirection.z)*speed*Time.deltaTime);
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
        currentCooldown -= Time.deltaTime;

    }

    private IEnumerator AttackPattern()
    {

        yield return Idle();

        while (this.player)
        {
            yield return Swoop();
            yield return Idle();
        }
    }

    private IEnumerator Idle()
    {
        this.state = EnemyState.Idle;
        
        yield return new WaitForSeconds(
            Random.Range(this.minWaitTime, this.maxWaitTime));
    }

    private IEnumerator Swoop()
    {
        this.state = EnemyState.Swoop;
        yield return new WaitForSeconds(2.5f);
        
    }

    private void OnControllerColliderHit(ControllerColliderHit hit){
        if(hit.gameObject.tag == this.damageTag){
            if(currentCooldown <= 0){
                var healthManager = hit.gameObject.GetComponent<HealthManager>();
                healthManager.ApplyDamage(this.damage);
                currentCooldown = damageCooldown;
            }
        }
    }

}
