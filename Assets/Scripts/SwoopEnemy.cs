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

    private PlayerController player;
    private Rigidbody rb;
    private EnemyState state;
    private Vector3 swoopDirection;

    private enum EnemyState {
        Idle,
        Swoop
    }

    private void Awake()
    {
        this.player = FindObjectOfType<PlayerController>();
        this.rb = GetComponent<Rigidbody>();
        StartCoroutine(AttackPattern());
    }

    private void FixedUpdate()
    {

        if (this.state == EnemyState.Idle)
        {
            this.rb.velocity = Vector3.zero;
            Vector3 swoopDistance = player.transform.position - this.transform.position;
            this.swoopDirection = swoopDistance.normalized;
            float swoopAngle = Mathf.Atan2(swoopDirection.z, swoopDirection.x)*Mathf.Rad2Deg - 90f;
            rb.rotation = Quaternion.Euler(0,-swoopAngle,0);
        }
        else if (this.state == EnemyState.Swoop && this.player)
        {
            this.rb.velocity = new Vector3(swoopDirection.x, 0, swoopDirection.z)*speed*Time.deltaTime;
        }
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

}
