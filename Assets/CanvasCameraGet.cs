using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class CanvasCameraGet : MonoBehaviour
{


    void Start()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
    }

}
