using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FramesController : MonoBehaviour
{
    private void Awake() {

        Application.targetFrameRate = 60;
    }
}
