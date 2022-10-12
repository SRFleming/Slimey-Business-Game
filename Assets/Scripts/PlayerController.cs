using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private GameObject projectilePrefab;


    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
            transform.Translate(Vector3.left * (this.speed * Time.deltaTime));
        if (Input.GetKey(KeyCode.D))
            transform.Translate(Vector3.right * (this.speed * Time.deltaTime));
        if (Input.GetKey(KeyCode.W))
            transform.Translate(Vector3.forward * (this.speed * Time.deltaTime));
        if (Input.GetKey(KeyCode.S))
            transform.Translate(Vector3.back * (this.speed * Time.deltaTime));

        if (Input.GetMouseButtonDown(0))
        {
            Plane plane = new Plane(Vector3.up, new Vector3(0,0,0));
            Vector3 mousePos = Input.mousePosition;
            Vector3 worldPosition = Vector3.down;
            float distance;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            if (plane.Raycast(ray, out distance)){
                worldPosition = ray.GetPoint(distance);
            }
            Vector3 projectileDir = Vector3.Normalize(worldPosition - gameObject.transform.position);
            var projectile = Instantiate(this.projectilePrefab);
            projectile.GetComponent<ProjectileController>().SetVelocity(projectileDir*10.0f);
            projectile.transform.position = gameObject.transform.position;
        }
    }
}