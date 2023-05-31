using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicAI : MonoBehaviour
{
    private GameObject left, right, up, down;

    private BoxCollider2D colliderL, colliderR, colliderU, colliderD;
 
    private Vector3 scale;




    private void Awake() {

        CreateSensors();

    }


    private void CreateSensors() {

        //------------------------------------
        left = new GameObject("Left");
        right = new GameObject("Right");
        up = new GameObject("Up");
        down = new GameObject("Down");
        //---------------------------------------

        scale = new Vector3(0.5f, 0.5f, 0.5f);


        //--------------------------------------------

        for (int i = 0; i < 4; i++) {

        left.transform.position = new Vector3(transform.position.x - 2, transform.position.y);           
        left.transform.parent = transform;
            

        right.transform.position = new Vector3(transform.position.x + 2, transform.position.y);           
        right.transform.parent = transform;

        up.transform.position = new Vector3(transform.position.x, transform.position.y + 2);  
        up.transform.parent = transform;

        down.transform.position = new Vector3(transform.position.x, transform.position.y - 2);          
        down.transform.parent = transform;

        }


        //-----------------------------------------------------

        colliderL = left.AddComponent<BoxCollider2D>();
        colliderL.isTrigger = true;
        colliderL.size = scale;

        colliderR = right.AddComponent<BoxCollider2D>();
        colliderR.isTrigger = true;
        colliderR.size = scale;

        colliderU = up.AddComponent<BoxCollider2D>();
        colliderU.isTrigger = true;
        colliderU.size = scale;

        colliderD = down.AddComponent<BoxCollider2D>();
        colliderD.isTrigger = true;
        colliderD.size = scale;


        //-----------------------------------------------------

        left.AddComponent<EnemySensors>();
        right.AddComponent<EnemySensors>();
        up.AddComponent<EnemySensors>();
        down.AddComponent<EnemySensors>();

    }



} // CLASS
