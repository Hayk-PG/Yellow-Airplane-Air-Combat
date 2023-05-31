using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthGameObjectScript : MonoBehaviour
{
    public int addToHealth;

    private Rigidbody2D rb;
    private HealthControllerScript healthControllerScript;

    private void Awake() {

        rb = GetComponent<Rigidbody2D>();
        healthControllerScript = GameObject.Find("HealthController").GetComponent<HealthControllerScript>();

    }
    private void Update() {


        addToHealth = Random.Range(10, 30);

        
       /* if(addToHealth >= 20 && addToHealth < 30) { addToHealth = 20; }
        if(addToHealth >= 30 && addToHealth < 40) { addToHealth = 30; }
        if(addToHealth >= 40 && addToHealth < 50) { addToHealth = 40; };
        if(addToHealth >= 50 && addToHealth < 60) { addToHealth = 50; };*/

       

    }


    private void OnTriggerEnter2D(Collider2D collision) {

        if(collision.gameObject.tag == "Player") {

            collision.gameObject.GetComponent<PlayerDamageScript>().health += addToHealth;
            GameObject.Find("HealthController").GetComponent<HealthControllerScript>().score = 0;

            healthControllerScript.canDamage = false; // PLAYER DAMAGE

            SoundManagerScript.instance.ChangeSoundFX("heartSound");

            Destroy(this.gameObject);

            //PARTICLE
            GameObject p = Resources.Load("HearthParticle") as GameObject;
            GameObject hearthParticles = Instantiate(p, transform.position, Quaternion.identity);
        }
        

    }


}//CLASS
