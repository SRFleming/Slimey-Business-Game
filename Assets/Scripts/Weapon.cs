using UnityEngine;

public class Weapon : MonoBehaviour
{   
    public GameObject projectilePrefab;
    public Transform muzzle;
    public float bulletSpeed, fireCooldown = 0.25f;
    public int numProjectiles = 5;
    public int damage;
    public bool automatic = true;
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
                    projectile.GetComponent<ProjectileController>().SetDamage(damage);
                    projectile.GetComponent<ProjectileController>().SetVelocity(bulletAngle*bulletSpeed);
                    bulletAngle = Quaternion.Euler(0, 10, 0)*bulletAngle;
                    currentCooldown = fireCooldown;
                }
            }
            else{
                GameObject projectile = Instantiate(this.projectilePrefab);
                projectile.transform.position = muzzle.position;
                projectile.GetComponent<ProjectileController>().SetDamage(damage);
                projectile.GetComponent<ProjectileController>().SetVelocity(aimDirection*bulletSpeed);
                currentCooldown = fireCooldown;
            }
        }
    }

    public void Update(){
        currentCooldown -= Time.deltaTime;
    }
}