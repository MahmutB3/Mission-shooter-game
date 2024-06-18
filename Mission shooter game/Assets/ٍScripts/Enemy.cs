
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
     [Header("Enemy health and damage")]
     private float enemyHealth = 120f;
     private float presentHealth;
     public float giveDamage = 5f;
     public HealthBar healthBar;

     [Header("Enemy things")]
     public NavMeshAgent enemyAgent;
     public Transform LookPoint;
     public Camera ShootingRaycastArea;
     public Transform playerBody;
     public LayerMask playerLayer;
     
     [Header("Enemy guarding")]
     public GameObject[] walkPoints;
     int currentEnemyPosition = 0;
     public float enemySpeed;
     float  walkingpointRadius = 2;
    // [Header("sounds and UI")]
     [Header("Enemy shooting var")]
     public float timebtwShoot;
     bool previouslyShoot; 

     [Header("Enemey animation and spark effect")]
     public Animator anim;
     public ParticleSystem muzzleSpark;
    
     [Header("Enemy mood/setuation")]
     public float visionRadius;
     public float shootingRadius;
     public bool playerInvisionRadius;
     public bool playerInshootingRadius;
     private void Awake(){
        presentHealth = enemyHealth;
        healthBar.GivefullHealth(enemyHealth);
        playerBody = GameObject.FindWithTag("Player").transform;
        enemyAgent = GetComponent<NavMeshAgent>();
     }
     private void Update(){
        playerInvisionRadius = Physics.CheckSphere(transform.position, visionRadius, playerLayer);
        playerInshootingRadius = Physics.CheckSphere(transform.position, shootingRadius, playerLayer);

        if(!playerInvisionRadius && !playerInshootingRadius) Guard();
        if(playerInvisionRadius && !playerInshootingRadius) Pursueplayer();
        if(playerInvisionRadius && playerInshootingRadius) ShootPlayer();

     }

    private void Guard()
    {
        if(Vector3.Distance(walkPoints[currentEnemyPosition].transform.position, transform.position) < walkingpointRadius){
            currentEnemyPosition = UnityEngine.Random.Range(0,walkPoints.Length);
            if(currentEnemyPosition >= walkPoints.Length){
                currentEnemyPosition = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, walkPoints[currentEnemyPosition].transform.position, Time.deltaTime * enemySpeed);
        transform.LookAt(walkPoints[currentEnemyPosition].transform.position);
    }
    private void Pursueplayer()
    {
        if(enemyAgent.SetDestination(playerBody.position)){

            anim.SetBool("Walk", false);
            anim.SetBool("AimRun", true);
            anim.SetBool("Shoot", false);
            anim.SetBool("Die", false);

            visionRadius = 30f;
            shootingRadius = 15f;
        }
        else    
        {
            anim.SetBool("Walk", false);
            anim.SetBool("AimRun", false);
            anim.SetBool("Shoot", false);
            anim.SetBool("Die", true);
        }
    }
        private void ShootPlayer()
        {
            enemyAgent.SetDestination(transform.position);
            transform.LookAt(LookPoint);

            if(!previouslyShoot)
            {
                muzzleSpark.Play();
                RaycastHit hit;
                if(Physics.Raycast(ShootingRaycastArea.transform.position, ShootingRaycastArea.transform.forward,out hit, shootingRadius)) 
                {
                    Debug.Log("Shooting" + hit.transform.name);
                    PlayerScript playerBody = hit.transform.GetComponent<PlayerScript>();
                    if(playerBody != null)
                    {
                        playerBody.playerHitDamage(giveDamage);
                    }
                    anim.SetBool("Walk", false);
                    anim.SetBool("AimRun", false);
                    anim.SetBool("Shoot", true);
                    anim.SetBool("Die", false);
                }
                previouslyShoot = true;
                Invoke(nameof(ActiveShooting), timebtwShoot);
            }
        }
        private void ActiveShooting()
        {
            previouslyShoot = false;
        }
        public void enemyHitDamage(float takeDamage){
            presentHealth -= takeDamage;
            healthBar.SetHealth(presentHealth);

            visionRadius = 40f;
            shootingRadius = 15f;
            
            if(presentHealth <= 0)
            {
                anim.SetBool("Walk", false);
                anim.SetBool("AimRun", false);
                anim.SetBool("Shoot", false);
                anim.SetBool("Die", true);

                enemyDie();
            }
        }
        private void enemyDie()
        {
            enemyAgent.SetDestination(transform.position);
            enemySpeed = 0f;
            shootingRadius = 0f;
            visionRadius = 0f;
            playerInvisionRadius = false;
            playerInshootingRadius = false;
            Object.Destroy(gameObject, 5.0f);
        }
}
