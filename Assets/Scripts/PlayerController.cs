using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject playerModel;
    [SerializeField] private float damageMultiplier, speedMultiplier, aSpeedMultiplier;
    private Rigidbody rBody;
    public Weapon weapon; 
    private Animator animator;
    private Vector3 aimDirection = Vector3.forward;
    private float moveDirectionX, moveDirectionZ, aimAngle;
    
    private void Start()
    {
        damageMultiplier = 1f;
        speedMultiplier = 1f;
        aSpeedMultiplier = 1f;
        rBody = this.GetComponent<Rigidbody>();
        animator = playerModel.GetComponent<Animator>();
    }
    
    private void Update()
    {
        moveDirectionX = Input.GetAxisRaw("Horizontal");
        moveDirectionZ = Input.GetAxisRaw("Vertical");

        if ((this.rBody.velocity != Vector3.zero)) {
            animator.SetBool("isRunning", true);
        } else {
            animator.SetBool("isRunning", false);
        }

        if(weapon.automatic){
          if (Input.GetMouseButton(0)){ weapon.Shoot(damageMultiplier, aSpeedMultiplier); }  
        }
        else if (Input.GetMouseButtonDown(0)){ weapon.Shoot(damageMultiplier, aSpeedMultiplier); }
    }

    private void FixedUpdate()
    {
        rBody.velocity = new Vector3(moveDirectionX,0,moveDirectionZ)*speed*speedMultiplier;
        Debug.Log(rBody.velocity);
        // calculate aim direction from mouse postion       
        Plane plane = new Plane(Vector3.up, new Vector3(0,transform.position.y,0));
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

    public void AddDamageMulti(float m){
        damageMultiplier += m;
    }
    public void AddASpeedMulti(float m){
        aSpeedMultiplier -= m;
    }
    public void AddSpeedMulti(float m){
        speedMultiplier += m;
    }

    public void OnCollisionEnter(Collision col){
        rBody.velocity = Vector3.zero;
    }
}