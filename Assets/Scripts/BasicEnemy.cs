using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private float damageCooldown;
    [SerializeField] private string damageTag;
   
    private CharacterController cC;
    private float currentCooldown;
    
    
    void Start(){
        cC = GetComponent<CharacterController>();
        player = FindObjectOfType<PlayerController>().transform;
    }

    void Update(){
        Vector3 direction = Vector3.Normalize(player.position - transform.position);
        cC.Move(direction*speed*Time.deltaTime);
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        float directionAngle = Mathf.Atan2(direction.z, direction.x)*Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0,-directionAngle,0);
        currentCooldown -= Time.deltaTime;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit){
        Debug.Log(hit.gameObject.tag);
        if(hit.gameObject.tag == this.damageTag){
            if(currentCooldown <= 0){
                var healthManager = hit.gameObject.GetComponent<HealthManager>();
                healthManager.ApplyDamage(this.damage);
                currentCooldown = damageCooldown;
            }
        }
    }
}