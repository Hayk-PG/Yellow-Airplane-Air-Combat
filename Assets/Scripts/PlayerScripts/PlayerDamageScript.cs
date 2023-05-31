using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerDamageScript : MonoBehaviour,IDamageScript 
{
    public float health = 100;
    public float damage = 10;

    public bool canDamage = false;

    private GameObject explosion;
  
    public void Damage() {

        if (GameObject.Find("HealthController").GetComponent<HealthControllerScript>().canDamage) {

            health -= damage;
        }
        

    }



    private void Update() {

        EnemyAirPlaneDestroy();

        CanDamage();

    }




    private void EnemyAirPlaneDestroy() {

        if (health <= 0) {

            explosion = Resources.Load("Explosion") as GameObject;
            GameObject explosionCopy = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);

            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShakeScript>().canShake = true;
        }

        if(health > 100) {

            health = 100;
        }


        if (health <= 25) {

            transform.GetChild(6).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(false);
        }
        else {

            transform.GetChild(6).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
        }
            

    }

    // AFTER  SECONDS DAMAGE IS ENABLED
    private void CanDamage() {

      
        if(Time.timeSinceLevelLoad < 5) {

            canDamage = false;
        }

        else if(Time.timeSinceLevelLoad >= 5) {

            canDamage = true;
        }
    }


  

} //CLASS
