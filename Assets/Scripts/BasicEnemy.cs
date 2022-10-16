using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private string damageTag;
    private Rigidbody rBody;
    
    
    
    
    void Start(){
        rBody = this.GetComponent<Rigidbody>();
    }

    void FixedUpdate(){
        Vector3 direction = Vector3.Normalize(player.position - transform.position);
        rBody.velocity = direction*speed;
        float directionAngle = Mathf.Atan2(direction.z, direction.x)*Mathf.Rad2Deg - 90f;
        rBody.rotation = Quaternion.Euler(0,-directionAngle,0);
    }

    private void OnCollisionEnter(Collision col){
        if(col.gameObject.tag == this.damageTag){
            var healthManager = col.gameObject.GetComponent<HealthManager>();
            healthManager.ApplyDamage(this.damage);
        }
    }
}