using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class ShootingEnemy : MonoBehaviour
{
    [SerializeField] private float minWaitTime;
    [SerializeField] private float maxWaitTime;
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private float damageCooldown, fireCooldown;
    [SerializeField] private string damageTag;
    [SerializeField] private float aimTime;
    [SerializeField] private float damageMultiplier, speedMultiplier;
    public GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed;


    private PlayerController player;
    private CharacterController cC;
    private EnemyState state;
    private Vector3 swoopDirection;
    private Vector3 aimDirection;
    private float currentCooldown;
    public Transform muzzle;

    private enum EnemyState {
        Idle,
        Swoop, 
        Attack
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

        if (this.state == EnemyState.Idle)
        {
            cC.Move(Vector3.zero);
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            Vector3 swoopDistance = player.transform.position - this.transform.position;
            this.swoopDirection = swoopDistance.normalized;
            float swoopAngle = Mathf.Atan2(swoopDirection.z, swoopDirection.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Euler(0, -swoopAngle, 0);
        }
        else if (this.state == EnemyState.Swoop && this.player)
        {
            cC.Move(new Vector3(swoopDirection.x, 0, swoopDirection.z) * speed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
        else if (this.state == EnemyState.Attack && this.player)
        {
            var target = player.transform.position;
            this.aimDirection = (target - this.transform.position).normalized;
        }
        currentCooldown -= Time.deltaTime;

    }

    private IEnumerator AttackPattern()
    {
    
        yield return Idle();

        while (this.player)
        {
            yield return Swoop();
            yield return Attack();
            yield return Idle();
        }
    }

    private IEnumerator Idle()
    {
        this.state = EnemyState.Idle;
        
        yield return new WaitForSeconds(Random.Range(this.minWaitTime, this.maxWaitTime));
    }

    private IEnumerator Swoop()
    {
        this.state = EnemyState.Swoop;

        yield return new WaitForSeconds(2.5f);
        
    }

    private IEnumerator Attack()
    {
        this.state = EnemyState.Attack;
        
        yield return new WaitForSeconds(this.aimTime);
        
        yield return Fire();
    }

    private IEnumerator Fire()
    {
        GameObject projectile = Instantiate(this.projectilePrefab);
        projectile.transform.position = muzzle.position;
        projectile.transform.rotation = transform.rotation;
        projectile.GetComponent<ProjectileController>().SetDamage(damage);
        projectile.GetComponent<ProjectileController>().SetVelocity(Vector3.forward * projectileSpeed);
        currentCooldown = fireCooldown;
    
        yield return new WaitForSeconds(0.5f); 
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == this.damageTag) {

            if (currentCooldown <= 0) {
                var healthManager = hit.gameObject.GetComponent<HealthManager>();
                healthManager.ApplyDamage(this.damage);
                currentCooldown = damageCooldown;
            }
        }
    }
}