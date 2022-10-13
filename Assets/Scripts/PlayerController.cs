using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private GameObject projectilePrefab;
    public Rigidbody rBody;
    public Weapon weapon; 
    private Vector3 aimDirection = Vector3.forward;
    private float moveDirectionX;
    private float moveDirectionZ;
    private float aimAngle;
    private void Update()
    {
        moveDirectionX = Input.GetAxisRaw("Horizontal");
        moveDirectionZ = Input.GetAxisRaw("Vertical");

        if (Input.GetMouseButtonDown(0))
        {
            weapon.Shoot(aimDirection);
        }
    }

    private void FixedUpdate()
    {
        rBody.velocity = new Vector3(moveDirectionX,0,moveDirectionZ)*speed;

        // calculate aim direction from mouse postion       
        Plane plane = new Plane(Vector3.up, new Vector3(0,0,0));
        Vector3 mousePos = Input.mousePosition;
        Vector3 mouseWorldPos = Vector3.down;
        float distance;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        if (plane.Raycast(ray, out distance)){
            mouseWorldPos = ray.GetPoint(distance);
        }
        aimDirection = Vector3.Normalize(mouseWorldPos - gameObject.transform.position); 
        
        // rotate player to follow mouse
        // aimAngle math from https://www.youtube.com/watch?v=LqrAbEaDQzc
        float aimAngle = Mathf.Atan2(aimDirection.z, aimDirection.x)*Mathf.Rad2Deg - 90f;
        rBody.rotation = Quaternion.Euler(0,-aimAngle,0);

    }
}