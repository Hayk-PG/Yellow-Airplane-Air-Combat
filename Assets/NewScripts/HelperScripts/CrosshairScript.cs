using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CrosshairScript : MonoBehaviour
{
    private Vector3 mousePosition;
    private Vector3 distance;
   
    private Transform player;

    public float magnitude;

    private Color a;


    private void Awake() {

        a = GetComponent<SpriteRenderer>().color;
        a.a = 0; 
    }



    private void Update() {

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        IsPointerOverUIObject();

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        distance = player.position - transform.position;
        magnitude = distance.magnitude;



        if (!IsPointerOverUIObject())
            transform.position = mousePosition;

    }


    // CHECKING IF TOUCHPOSITION EQAULS GUI BUTTON POSITION OR NOT
    private bool IsPointerOverUIObject() {

        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;

    }



} //CLASS
