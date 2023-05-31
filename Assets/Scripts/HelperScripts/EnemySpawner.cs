using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private Transform player;

    public Transform[] spawnPoints;
    public GameObject enemies;

    private GameObject enemyCopy;
   
    public int pointIndex;
    public int spawnCount = 1;
    public int airPlaneCount;
    public float enemyRotationLerp = 0;
    private Quaternion direction;

    private void Update() {

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        transform.position = new Vector3(player.position.x + Random.Range(-5, 5), player.position.y + Random.Range(-5, 5));

        // NUMBER OF ENEMIES

        airPlaneCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        //---------------------------------------------------------------------------

        Spawn();
    }

    private void Spawn() {

        if(airPlaneCount < 3) {

            enemyRotationLerp += 0.001f;

            for (pointIndex = 0; pointIndex < spawnPoints.Length; pointIndex++) {

                switch (pointIndex) {

                    case 0: direction = Quaternion.AngleAxis(0, Vector3.forward); break;
                    case 1: direction = Quaternion.AngleAxis(0, Vector3.forward); break;
                    case 2: direction = Quaternion.AngleAxis(0, Vector3.forward); break;
                  
                }

                

                enemyCopy = Instantiate(enemies, spawnPoints[pointIndex].position, direction);
            }
        }
    }
}
