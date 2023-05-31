using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShootScript : MonoBehaviour
{
    public GameObject bullet;
    private GameObject bulletCopy;
    public GameObject bulletsHolder;

    public Transform shootPoint;
    public Transform shootDirection;

    private float rateOfFire;
    private float rofTime;// + RATE OF FIRE
  
    private Rigidbody2D rb;

    public bool canShoot;
    public bool stillCanShoot;

    public Image barrelIcon;

    private void Awake() {

        barrelIcon.color = Color.green;
    }

    private void Update() {


        //----------------------------------------------------------------
        if (stillCanShoot) {

            barrelIcon.fillAmount -= 0.2f * Time.deltaTime;
        }
        else
            barrelIcon.fillAmount += 0.5f * Time.deltaTime;


        if(barrelIcon.fillAmount <= 0.16f) {

            barrelIcon.fillAmount = 0.16f;
        }

        else if (barrelIcon.fillAmount >= 0.9f) {

            barrelIcon.fillAmount = 0.9f;
        }

        if(barrelIcon.fillAmount <= 0.2f) {

            barrelIcon.color = Color.red;
        }

            if (barrelIcon.fillAmount <= 0.6f && barrelIcon.fillAmount > 0.2f) {

            barrelIcon.color = Color.yellow;
            rofTime = 0.3f;
        }
        else if (barrelIcon.fillAmount > 0.6f) {

            barrelIcon.color = Color.green;
            rofTime = 0.1f;
        }
        //------------------------------------------------------------------








    }



    private void FixedUpdate() {

       
        if (canShoot && barrelIcon.fillAmount >= 0.2f) {

            if (Time.time > rateOfFire) {

                rateOfFire = Time.time + rofTime;

                Shoot();
            }

        }


    }

    private void Shoot() {

        bulletCopy = Instantiate(bullet,shootPoint.position,transform.rotation);
        bulletCopy.transform.parent = bulletsHolder.transform;
        rb = bulletCopy.GetComponent<Rigidbody2D>();

        rb.AddForce((shootDirection.position - shootPoint.position) * 50000 * Time.deltaTime);

        
    }


    public void CanShoot(bool canShoot) {

        this.canShoot = canShoot;

    }


    public void BarrelHeatController(bool stillCanShoot) {

        this.stillCanShoot = stillCanShoot;
    }


}// CLASS
