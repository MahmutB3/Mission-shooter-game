using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swap_rifle : MonoBehaviour
{
    [Header("Rifle")]
    public GameObject Weapon;
    public GameObject PlayerRifle1;
    public GameObject PlayerRifle2;
    public GameObject PickupRifle1;
    public GameObject PickupRifle2;

    [Header("Rifle Assign Things")]
    public PlayerScript player;
    public float radius = 0.5f;

    private void Awake(){
        Weapon.SetActive(false);
    }
    private void Update(){
        if(Vector3.Distance(transform.position, player.transform.position )< radius){
            if(Input.GetKeyDown("f")){
                Weapon.SetActive(true);
                PlayerRifle1.SetActive(true);
                PlayerRifle2.SetActive(false);
                PickupRifle1.SetActive(false);
                PickupRifle2.SetActive(true);
            }
        }
    }
}
