using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 30.0f;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject playerModel;
    [SerializeField] private float damageMultiplier, speedMultiplier, aSpeedMultiplier;
    private CharacterController cC;
    private Rigidbody rBody;
    public Weapon weapon; 
    private Animator animator;
    private Vector3 aimDirection = Vector3.forward;
    private Vector3 normalizedmovement;
    private float moveDirectionX, moveDirectionZ, aimAngle;
    
    private void Start()
    {
        damageMultiplier = 1f;
        speedMultiplier = 1f;
        aSpeedMultiplier = 1f;
        // rBody = this.GetComponent<Rigidbody>();
        cC = this.GetComponent<CharacterController>();
        animator = playerModel.GetComponent<Animator>();
        gameObject.tag = "Player";
    }
    
    private void Update()
    {
        // moves player based off input, chooses animation
        moveDirectionX = Input.GetAxisRaw("Horizontal");
        moveDirectionZ = Input.GetAxisRaw("Vertical");
        normalizedmovement = new Vector3(moveDirectionX, 0, moveDirectionZ);
        normalizedmovement.Normalize();
        cC.Move(normalizedmovement * speed * speedMultiplier * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        
        if ((moveDirectionX != 0 || moveDirectionZ != 0)) {
            animator.SetBool("isRunning", true);
        } else {
            animator.SetBool("isRunning", false);
        }
        
        // rotates player to follow mouse
        // aimAngle math from https://www.youtube.com/watch?v=LqrAbEaDQzc
        Plane plane = new Plane(Vector3.up, new Vector3(0,transform.position.y,0));
        Vector3 mousePos = Input.mousePosition;
        Vector3 mouseWorldPos = Vector3.down;
        float distance;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        if (plane.Raycast(ray, out distance)){
            mouseWorldPos = ray.GetPoint(distance);
        }
        aimDirection = Vector3.Normalize(mouseWorldPos - gameObject.transform.position); 
        float aimAngle = Mathf.Atan2(aimDirection.z, aimDirection.x)*Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0,-aimAngle,0);

        if (weapon.automatic){
          if (Input.GetMouseButton(0)){ weapon.Shoot(damageMultiplier, aSpeedMultiplier); }  
        }
        else if (Input.GetMouseButtonDown(0)){ weapon.Shoot(damageMultiplier, aSpeedMultiplier); }
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

    public void DestroyGameObject()
    {
        Destroy(this);
    }
}