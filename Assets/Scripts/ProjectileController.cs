using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private Vector3 velocity;

    private void Update()
    {
        transform.Translate(this.velocity * Time.deltaTime);
    }

    public void SetVelocity(Vector3 v){
        this.velocity = v;
    }
    /* 
    private void OnTriggerEnter(Collider col)
    {

    } */
}