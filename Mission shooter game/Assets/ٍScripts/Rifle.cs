using System.Collections;
using UnityEngine;

public class Rifle : MonoBehaviour
{
    [Header("Rifle Things")]
    public Camera Camera;
    public float giveDamageOf = 10f;
    public float shootingRange = 100f;
    public float fireCharge = 10f;
    public Animator animator;
    public PlayerScript player;

    [Header("Rifle Ammunition and shooting")]
    private int maximumAmmunition = 20;
    private int mag = 15;
    private int presentAmmunition;
    public float reloadingTime = 1.3f;
    private bool setReloading = false;
    private float nextTimeToShoot = 0f;

    [Header("Rifle Effects")]
    public ParticleSystem muzzleSpark;
    public GameObject impactEffect;
    public GameObject goreEffect;


    [Header("Sounds nad UI")]
    [SerializeField] private GameObject AmmoOutUI;
    [SerializeField] private int timeToShowUI = 1;
    public AudioClip shootingSound;
    public AudioClip reloadingSound;
    public AudioSource audioSource;

    private void Awake()
    {
        presentAmmunition = maximumAmmunition;
    }
    void Update()
    {
        if(setReloading)
        return;

        if(presentAmmunition <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

            
        if(Input.GetButton("Fire1") && Time.time >= nextTimeToShoot)
        {
            animator.SetBool("Fire", true);
            animator.SetBool("Idle", false);
            nextTimeToShoot = Time.time + 1f/fireCharge;
            shoot();
        }


        else if(Input.GetButton("Fire1") && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            animator.SetBool("Idle", false);
            animator.SetBool("IdleAim", true);
            animator.SetBool("FireWalk", true);
            animator.SetBool("Walk", true);
            animator.SetBool("Reloading", false);
        }

        else if(Input.GetButton("Fire2") && Input.GetButton("Fire1"))
        {
            animator.SetBool("Idle", false);
            animator.SetBool("IdleAim", true);
            animator.SetBool("FireWalk", true);
            animator.SetBool("Walk", true);
            animator.SetBool("Reloading", false);
        }

        else
        {
            animator.SetBool("Fire", false);
            animator.SetBool("Idle", true);
            animator.SetBool("FireWalk", false);
            animator.SetBool("Reloading", false);
        }
    }

    void shoot()
    {
        if(mag == 0)
        {
            // show ammo out text
            StartCoroutine(ShowAmmouOut());
            return;
        }
        presentAmmunition--;

        if(presentAmmunition == 0)
        {
            mag--;
        }

        //Update UI
        AmmoCount.occurrence.UpdateAmmoText(presentAmmunition);
        AmmoCount.occurrence.UpdateMagText(mag);

        muzzleSpark.Play();
        audioSource.PlayOneShot(shootingSound);
        RaycastHit hitInfo;

        
        if(Physics.Raycast(Camera.transform.position, Camera.transform.forward, out hitInfo, shootingRange))
        {
            Debug.Log(hitInfo.transform.name);

            Objects objects = hitInfo.transform.GetComponent<Objects>();
            Enemy enemy = hitInfo.transform.GetComponent<Enemy>();

            if(objects != null)
            {
                objects.objectHitDamage(giveDamageOf);
                GameObject impactGO = Instantiate(impactEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(impactGO, 1f);
            }   
            else if(enemy != null)
            {
                enemy.enemyHitDamage(giveDamageOf);
                GameObject impactGO = Instantiate(goreEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(impactGO, 1f);
            }
        }
    }
    IEnumerator Reload()
    {
        player.playerSpeed = 0f;
        player.playerSprint = 0f;
        setReloading = true;
        Debug.Log("Reloading...");
        animator.SetBool("Reloading", true);
        audioSource.PlayOneShot(reloadingSound);
        yield return new WaitForSeconds(reloadingTime);
        animator.SetBool("Reloading", false);
        presentAmmunition = maximumAmmunition;
        player.playerSpeed = 2f;
        player.playerSprint = 4f;
        setReloading = false;
    }

    IEnumerator ShowAmmouOut()
    {
        AmmoOutUI.SetActive(true);
        yield return new WaitForSeconds(timeToShowUI);
        AmmoOutUI.SetActive(false);
    }
}
