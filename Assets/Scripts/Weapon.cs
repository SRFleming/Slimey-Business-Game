using UnityEngine;

public class Weapon : MonoBehaviour
{   
    public GameObject projectilePrefab;
    public Transform muzzle;
    public float bulletSpeed;


    public void Shoot(Vector3 aimDirection)
    {
        GameObject projectile = Instantiate(this.projectilePrefab);
        projectile.transform.position = muzzle.position;
        projectile.GetComponent<ProjectileController>().SetVelocity(aimDirection*bulletSpeed);
    }
}