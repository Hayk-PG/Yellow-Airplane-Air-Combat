using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearthParticleDestroy : MonoBehaviour
{
    private void Update() {

        Destroy(this.gameObject, 1);
    }
}
