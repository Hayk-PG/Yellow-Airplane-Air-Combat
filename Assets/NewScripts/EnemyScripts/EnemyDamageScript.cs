using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageScript : MonoBehaviour,IDamageScript
{
    public float health = 100;
    public float damage = 5;

    public GameObject trail;
   
    private GameObject explosion;
    public GameObject score25;

    private GameObject hearth;
  
    private Animator anim;
    public void Damage() {

        health -= damage;
        SoundManagerScript.instance.ChangeSoundFX("hit");
    }

    private void Awake() {

        anim = GameObject.Find("CameraAnimator").GetComponent<Animator>();

    }

    private void Update() {

        score25 = Resources.Load("ScorePointParent") as GameObject;
        hearth = Resources.Load("HearthParent") as GameObject;

        EnemyAirPlaneDestroy();
    }




    private void EnemyAirPlaneDestroy() {

        if(health <= 0) {

            explosion = Resources.Load("Explosion") as GameObject;
            GameObject explosionCopy = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            //GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShakeScript>().canShake = true;

            GameObject.Find("ScoreController").GetComponent<ScoreScript>().scorePoint += 25; // ADDING SCOREPOINT
            GameObject score = Instantiate(score25, transform.position, Quaternion.identity); // +25 ANIMATION

            //anim.SetTrigger("score"); // UI SCORE ANIMATION
            anim.Play("UIScoreAnimation", 0);
          
            GameObject.Find("HealthController").GetComponent<HealthControllerScript>().score += 100; // HEALTH HCONTROLLER SCRIPT

            // INSTANTIATING HEARTH OBJECT
            if (GameObject.Find("HealthController").GetComponent<HealthControllerScript>().heartSpawn) {
                print("hearth");
                GameObject hearthCopy = Instantiate(hearth, new Vector3(transform.position.x,transform.position.y + 2) , Quaternion.identity);
                GameObject[] h = GameObject.FindGameObjectsWithTag("Hearth");
                if (h.Length > 1) {

                    Destroy(h[0]);
                }

                else
                    Destroy(hearthCopy, 5);
                
            }


            health = 100;

        }


        if (health <= 25) {

            transform.GetChild(9).gameObject.SetActive(true);
            trail.SetActive(false);
        }
        else {

            transform.GetChild(9).gameObject.SetActive(false);
            trail.SetActive(true);
        }
            

    }



 


} // CLASS
