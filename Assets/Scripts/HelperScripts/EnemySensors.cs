using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySensors : MonoBehaviour {


    private GameObject airPlane;

    private GameObject blue;

    private float x, y;

    [SerializeField] private float distance;
  
    [SerializeField] private bool blueUp, blueDown, blueBack, blueFront;




    private void Update() {

        BlueController();

        x = transform.position.y;
        y = transform.position.y;

       



        

    }
    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.CompareTag("Enemy")) {

            airPlane = collision.gameObject;

            #region
            if (this.gameObject.name.Equals("Left")) {

                //print(this.gameObject.transform.parent.name);

                {
                    if (this.gameObject.transform.parent.name == "EnemyBlue(Clone)") {

                        blue = GameObject.Find("EnemyBlue(Clone)");
                        
                        //print(blue);

                        StartCoroutine(ActivateBool("blueBack"));

                    }
                }

            }
            #endregion

            #region
            if (this.gameObject.name.Equals("Right")) {

                //print(this.gameObject.transform.parent.name);

                {
                    if (this.gameObject.transform.parent.name == "EnemyBlue(Clone)") {

                        blue = GameObject.Find("EnemyBlue(Clone)");

                        //print(blue);

                        StartCoroutine(ActivateBool("blueFront"));


                    }

                }


            }
            #endregion















        }





    }



    IEnumerator ActivateBool(string boolName) {

        switch (boolName) {

            case "blueBack": blueBack = true; break;
            case "blueFront": blueFront = true; break;
        }

        yield return new WaitForSeconds(5);

       
        blueBack = false;
        blueFront = false;

        

    }



    private void BlueController() {

        GameObject.Find("EnemyBlue(Clone)").GetComponent<EnemyAirPlaneMovement>().magnitudeDistance = distance;
        blue = GameObject.Find("EnemyBlue(Clone)");

        if (blueBack) {

            blue.transform.Rotate(Vector3.forward * (45 * Time.deltaTime));
            
            
        }

        if (blueFront) {

            blue.transform.Rotate(Vector3.forward * (-45 * Time.deltaTime));
         
        }


        if (blueBack || blueFront) {

            distance = 0;
        }
        else {

            distance = 20;
        }












    }


















}
