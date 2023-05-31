using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] Text healthBarText;
  
    private Image image;


    private void Update() {

        health = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDamageScript>().health;
        image = GetComponent<Image>();


        image.fillAmount = health / 100;
        healthBarText.text = health + "/100";

    }







}// CLASS
