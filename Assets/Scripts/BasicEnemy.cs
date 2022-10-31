using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private string damageTag;
   
    private CharacterController cC;
    
    
    void Start(){
        cC = GetComponent<CharacterController>();
        player = FindObjectOfType<PlayerController>().transform;
    }

    void Update(){
        if (player == null) {
            return;
        }
        Vector3 direction = Vector3.Normalize(player.position - transform.position);
        cC.Move(direction*speed*Time.deltaTime);
        float directionAngle = Mathf.Atan2(direction.z, direction.x)*Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0,-directionAngle,0);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit){
        if(hit.gameObject.tag == this.damageTag){
            if(hit.gameObject.GetComponent<PlayerController>().currentCooldown <= 0){
                hit.gameObject.GetComponent<PlayerController>().IFrames();
                var healthManager = hit.gameObject.GetComponent<HealthManager>();
                healthManager.ApplyDamage(this.damage);
            }
        }
    }
}