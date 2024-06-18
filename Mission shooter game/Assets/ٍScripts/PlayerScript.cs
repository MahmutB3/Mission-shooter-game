
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Player Movement")]
    public float playerSpeed = 2f;
    public float playerSprint = 5.5f;

    [Header("Player Heals Things")]
    private float playerHealth = 120f;
    private float presentHealth;
    public HealthBar healthBar;
    public AudioClip playerHurtSound;
    public AudioSource audioSource;



    [Header("Player script cameras")]
    public Transform playerCamera;
    public GameObject deathCamera;
    public GameObject EndGameMenuUI;

    [Header("Rifle")]
    public GameObject Rifle;
    

    [Header("Player Animator and Gravity")]
    public CharacterController cC;
    public float gravity = -9.81f;
    public Animator animator;

    [Header("Player Jumping and velocity")]
    public float jumpRange = 1f;
    Vector3 velocity;
     public float turnCalmTime = 0.1f;
    float turnCalmVelocity;
    public Transform surfaceCheck;
    bool onSurface;
    public float surfaceDistance = 0.4f;
    public LayerMask surfaceMask;
    // Start is called before the first frame update
    void Start()
    {  
         Cursor.lockState = CursorLockMode.Locked;
         presentHealth = playerHealth;
         healthBar.GivefullHealth(playerHealth);
    }
    // Update is called once per frame
    void Update()
    {
        
        onSurface = Physics.CheckSphere(surfaceCheck.position, surfaceDistance, surfaceMask);

        if(onSurface && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        //grvity 
        velocity.y += gravity * Time.deltaTime;
        cC.Move(velocity * Time.deltaTime);


        playerMove();
        Jump();
        Sprint();   
    }
    void playerMove()
    {
        float horizontal_axis = Input.GetAxisRaw("Horizontal");
        float vertical_axis = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal_axis, 0f, vertical_axis).normalized;
        
        bool IsRifleActie = Rifle.activeSelf;

        if (direction.magnitude >= 0.1f)
        {
            animator.SetBool("Walk", IsRifleActie);
            animator.SetBool("Walk00", !IsRifleActie);
            animator.SetBool("Running", false);
            animator.SetBool("Running00", false);
            animator.SetBool("Idle", false);
            animator.SetTrigger("Jump");
            animator.SetBool("AimWalk", false);
            animator.SetBool("IdleAim", false);

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime);

            // Yatay ve dikey eksende karakterin hareket yönüne bağlı olarak atış animasyonlarını ayarla
            if (horizontal_axis > 0)
            {
                animator.SetBool("RightShoot", true);
                animator.SetBool("LeftShoot", false);
            }
            else if (horizontal_axis < 0)
            {
                animator.SetBool("RightShoot", false);
                animator.SetBool("LeftShoot", true);
            }
            else
            {
                animator.SetBool("RightShoot", false);
                animator.SetBool("LeftShoot", false);
            }

            if (vertical_axis < 0)
            {
                animator.SetBool("BackShoot", true);
            }
            else
            {
                animator.SetBool("BackShoot", false);
            }

            // Eğer karakter sola, sağa ya da arkaya hareket ediyorsa, ateş tuşuna basıldığında rotasyonu değiştirme
            if ((!Input.GetButton("Fire2") || Input.GetButton("Fire1")))
            {
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
            }
            
            if (Input.GetButton("Fire2") || Input.GetButton("Fire1"))
            {
                float targetAngle1 = playerCamera.eulerAngles.y;
                
                // Dönüş rotasyonunu ayarla
                transform.rotation = Quaternion.Euler(0f, targetAngle1, 0f);    
            }

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            cC.Move(moveDirection.normalized * playerSpeed * Time.deltaTime);
        }
        else
        {
            if (Input.GetButton("Fire2") || Input.GetButton("Fire1"))
            {
                float targetAngle2 = playerCamera.eulerAngles.y;
                float angle2 = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle2, ref turnCalmVelocity, turnCalmTime);
                // Dönüş rotasyonunu ayarla
                transform.rotation = Quaternion.Euler(0f, angle2, 0f); 
            }
            animator.SetBool("Idle", true);
            animator.SetTrigger("Jump");
            animator.SetBool("Walk", false);
            animator.SetBool("Walk00", false);
            animator.SetBool("Running", false);
            animator.SetBool("Running00", false);
            animator.SetBool("AimWalk", false);
            animator.SetBool("RightShoot", false);
            animator.SetBool("LeftShoot", false);
            animator.SetBool("BackShoot", false);
        }
    }

    

    void Jump()
    {
        if(Input.GetButtonDown("Jump") && onSurface)
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Walk00", false);
            animator.SetTrigger("Jump");

            velocity.y = Mathf.Sqrt(jumpRange * -2 * gravity);
        }
        else
        {
            animator.ResetTrigger("Jump");
        }
    }

    void Sprint()
    {
        if(Input.GetButton("Sprint") && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) && onSurface)
        {
            float horizontal_axis = Input.GetAxisRaw("Horizontal");
            float vertical_axis = Input.GetAxisRaw("Vertical");

            Vector3 direction = new Vector3(horizontal_axis, 0f, vertical_axis).normalized;
            bool IsRifleActie = Rifle.activeSelf;

            if(direction.magnitude >= 0.1f)
            {
                animator.SetBool("Running", IsRifleActie);
                animator.SetBool("Running00", !IsRifleActie);
                animator.SetBool("Idle", false);
                animator.SetBool("Walk", false);
                animator.SetBool("Walk00", false);
                animator.SetBool("IdleAim", false);
                

                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                
                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                cC.Move(moveDirection.normalized * playerSprint * Time.deltaTime);
            }
            else
            {
                animator.SetBool("Idle", false);
                animator.SetBool("Walk", false);
                animator.SetBool("Walk00", false);
            }
        }
        
    }
    public void playerHitDamage(float takeDamage)
    {
        presentHealth -= takeDamage;
        healthBar.SetHealth(presentHealth);
        audioSource.PlayOneShot(playerHurtSound);
        if(presentHealth <= 0)
        {
            playerDie();
        }
    }
    private void playerDie()
    {
        EndGameMenuUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;

        deathCamera.SetActive(true);
        Object.Destroy(gameObject, 1.0f);   
    }

}
