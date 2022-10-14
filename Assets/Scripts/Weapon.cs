using UnityEngine;

public class Weapon : MonoBehaviour
{   
    [SerializeField] public GameObject projectilePrefab;
    [SerializeField] public Transform muzzle;
    [SerializeField] public float bulletSpeed, fireCooldown = 0.25f;
    [SerializeField] public int numProjectiles = 5;
    [SerializeField] public bool automatic = true;
    private float currentCooldown;


    public void Start()
    {
        currentCooldown = fireCooldown;
    }

    public void Shoot(Vector3 aimDirection)
    {   
        if(currentCooldown <= 0.0f){
            if(numProjectiles>1){
                Vector3 bulletAngle = Quaternion.Euler(0, -(numProjectiles/2)*10, 0)*aimDirection;
                for(int i=0; i < numProjectiles; i++){
                    GameObject projectile = Instantiate(this.projectilePrefab);
                    projectile.transform.position = muzzle.position;
                    projectile.GetComponent<ProjectileController>().SetVelocity(bulletAngle*bulletSpeed);
                    bulletAngle = Quaternion.Euler(0, 10, 0)*bulletAngle;
                    currentCooldown = fireCooldown;
                }
            }
            else{
                GameObject projectile = Instantiate(this.projectilePrefab);
                projectile.transform.position = muzzle.position;
                projectile.GetComponent<ProjectileController>().SetVelocity(aimDirection*bulletSpeed);
                currentCooldown = fireCooldown;
            }
        }
    }

    public void Update(){
        currentCooldown -= Time.deltaTime;
    }
}