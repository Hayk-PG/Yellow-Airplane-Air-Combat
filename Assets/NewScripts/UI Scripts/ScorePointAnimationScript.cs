using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePointAnimationScript : MonoBehaviour
{
    public void Destroy() {

        Destroy(transform.parent.gameObject);
    }
}
