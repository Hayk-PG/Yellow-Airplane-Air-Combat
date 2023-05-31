using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootScript : MonoBehaviour
{
    private GameObject bullet;
    private GameObject bulletCopy;
    private GameObject bulletsHolder;

    public Transform shootPoint;
    public Transform shootDirection;

    private float rateOfFire;

    private Rigidbody2D rb;
    private AudioSource bulletSound;

    private RaycastHit2D hit;
    private RaycastHit2D cantHit;

    public LayerMask player;
    public LayerMask enemy;

    public bool canShoot;

    private void Update() {

        hit = Physics2D.Raycast(shootPoint.position, shootDirection.position - shootPoint.position, 10,player);
        cantHit = Physics2D.Raycast(shootPoint.position, shootDirection.position - shootPoint.position, 5, enemy);

        Debug.DrawRay(shootPoint.position, (shootDirection.position - shootPoint.position) * 10, Color.red);
        Debug.DrawRay(shootPoint.position, (shootDirection.position - shootPoint.position) * 2, Color.yellow);

        bullet = Resources.Load("Bullet") as GameObject;

       // Debug.DrawRay(shootPoint.position,(shootDirection.position - shootPoint.position) * 5,Color.white);

        // Can shoot

        if (cantHit) {

            canShoot = false;
        }
        else {

            canShoot = true;
        }
       

       
    }



    private void FixedUpdate() {


        // Fire Bullets

        if (hit && canShoot) {

            if (Time.time > rateOfFire) {

                rateOfFire = Time.time + 0.1f;

                Shoot();
            }
        }


    }




    private void Shoot() {

        bulletCopy = Instantiate(bullet, shootPoint.position, transform.rotation);

        bulletsHolder = GameObject.Find("BulletsHolder");
        bulletCopy.transform.parent = bulletsHolder.transform;
        rb = bulletCopy.GetComponent<Rigidbody2D>();
        bulletSound = bulletCopy.GetComponentInChildren<AudioSource>();
        bulletSound.volume = 0.06f;

        rb.AddForce((shootDirection.position - shootPoint.position) * 50000 * Time.deltaTime);
    }






} //CLASS
