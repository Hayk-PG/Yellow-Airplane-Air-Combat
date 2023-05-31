using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollorScript : MonoBehaviour
{
    private Color a, b, c;

    private SpriteRenderer sp;

    private int colorNumbers;




    private void Start() {

        colorNumbers = Random.Range(0, 3);
        sp = transform.GetChild(0).GetComponent<SpriteRenderer>();

        a = new Color(0, 201, 214);
        b = new Color(214, 0, 31);
        c = new Color(26, 214, 0);

        ChangeColors(colorNumbers);
    }




    private void Update() {

        

    }





    private void ChangeColors(int colorNumbers) {

        colorNumbers = this.colorNumbers;

        switch (colorNumbers) {

            case 0: sp.color = a; break;
            case 1: sp.color = b; break;
            case 2: sp.color = c; break;
        }

    }







} // CLASS
