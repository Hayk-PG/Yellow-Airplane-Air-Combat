using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{

    public float speed;
    public string name;

    public float enemyDamage;


    private void Awake() {

        Speed(name);
    }


    public void Speed(string name) {

        this.name = name;

        switch (name) {

            case "EnemyOrange(Clone)": speed = 100; break;
                
            case "EnemyGreen(Clone)": speed = 50; break;

            case "EnemyBlue(Clone)": speed = 90; break;

            case "EnemyRed(Clone)": speed = 120; break;

        }

    }

    public void Damage(string name) {

        this.name = name;
 
        switch (name) {

            case "EnemyOrange(Clone)": enemyDamage = 25; break;

            case "EnemyGreen(Clone)": enemyDamage = 40; break;

            case "EnemyBlue(Clone)": enemyDamage = 45; break;

            case "EnemyRed(Clone)": enemyDamage = 10; break;
        }


    }

} //CLASS
