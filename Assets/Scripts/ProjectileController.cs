using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private Vector3 velocity;
    [SerializeField] private string damageTag;
    [SerializeField] private string damageTag2;
    private int damage;
    private float time = 3f;
    private float remTime;

    private void Start(){
        remTime = time;
    }

    public void SetVelocity(Vector3 v){
        this.velocity = v;
    }

    public void SetDamage(int d){
        this.damage = d;
    }
     
    private void Update()
    {
        transform.Translate(this.velocity * Time.deltaTime);
        remTime -= Time.deltaTime;
        if(remTime <= 0){
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider col)
    {   
        if(col.gameObject.tag == damageTag || col.gameObject.tag == damageTag2){
            var healthManager = col.gameObject.GetComponent<HealthManager>();
            healthManager.ApplyImpactDamage(this.damage, this.transform.rotation);
        }
        Destroy(gameObject);
    } 
}