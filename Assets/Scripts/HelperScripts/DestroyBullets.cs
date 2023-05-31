using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullets : MonoBehaviour
{
    private GameObject hitParticles;


    private void Update() {

        Destroy(this.gameObject,0.5f);
    }

    private void OnCollisionEnter2D(Collision2D collision) {

        if(collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Player")) {

            IDamageScript damageScript = collision.gameObject.GetComponent<IDamageScript>();

            if (damageScript != null && GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDamageScript>().canDamage) {

                damageScript.Damage();

                hitParticles = Resources.Load("HitParticles") as GameObject;
                GameObject hitCopy = Instantiate(hitParticles, this.gameObject.transform.position, Quaternion.identity);
                Destroy(hitCopy, 0.2f);

                
            }


           

        }

    }
}
