using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurbulenceSript : MonoBehaviour
{

    private void Start() {

        StartCoroutine(Turbulence(Random.Range(0, 1), Random.Range(1, 3), Random.Range(-0.4f, 0.4f)));
    }

    private IEnumerator Turbulence(float time,float delay,float point) {

        yield return new WaitForSeconds(time);

        while(delay > 0) {

            delay -= Time.deltaTime;
            float y = transform.position.y;
            y += point * Time.deltaTime;
            transform.position = new Vector2(transform.position.x, y);

            yield return null;

        }

        yield return StartCoroutine(Turbulence(Random.Range(1, 5), Random.Range(1, 5), Random.Range(-1, 1)));
    }
}
