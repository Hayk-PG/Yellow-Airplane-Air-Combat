using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemies : MonoBehaviour
{
    private EnemiesController controller;

    private float damage;
    public float health = 100;

    private void Awake() {

        controller = GameObject.Find("EnemiesController").GetComponent<EnemiesController>();
    }

    private void Update() {

        controller.Damage(this.gameObject.name);
        damage = controller.enemyDamage;

        //---------------------------------------

        Destroy();

    }


    private void OnCollisionEnter2D(Collision2D collision) {
        
        if(collision.gameObject.tag == "PlayerBullet") {

            health -= damage;

        }
    }

    private void Destroy() {
        
        if(health <= 0) {

            Destroy(gameObject);

            health = 100;
        }
    }


} // CLASS
