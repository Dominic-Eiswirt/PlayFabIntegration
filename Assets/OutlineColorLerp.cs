using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class OutlineColorLerp : MonoBehaviour
{
    public Color from;
    public Color to;
    public Outline outline;
    WaitForSeconds waitDuration = new WaitForSeconds(0.05f);
    private void Start()
    {
        StartCoroutine(LerpColor());
    }
   
    IEnumerator LerpColor()
    {
        while (true)
        {
            outline.effectColor = Color.Lerp(from, to, Mathf.PingPong(Time.time, 1));
            yield return waitDuration;
        }
    }

}
