using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthControllerScript : MonoBehaviour
{
    public bool heartSpawn;

    public bool canDamage;//CAN PLAYER GET DAMAGE
    [SerializeField] private int t; // TIME AFTER PLAYER CAN GET DAMAGE

    [SerializeField] private float playerHealth;
    
    public float score;



    private void Awake() {

        canDamage = true;

    }

    private void Start() {

        StartCoroutine(CanDAMAGE());
    }





    private void Update() {

        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDamageScript>().health;

        if (score >= 50 && playerHealth <= 70) {

            heartSpawn = true;
        }

        else
            heartSpawn = false;




    }

    //CAN PLAYER GET ANY DAMAGE
    private IEnumerator CanDAMAGE() {

        if (!canDamage) {

            yield return new WaitForSeconds(3);

            canDamage = true;
        }

        yield return null;

        StartCoroutine(CanDAMAGE());
    }





}// CLASS
