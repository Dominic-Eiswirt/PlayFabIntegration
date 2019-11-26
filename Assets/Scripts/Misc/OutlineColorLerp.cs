using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class OutlineColorLerp : MonoBehaviour
{
    public Color from = new Color(0, 0, 0, 1);
    public Color to = new Color(0, 0, 0, 1);
    public Outline outline;

    private WaitForSeconds waitDuration = new WaitForSeconds(0.075f);

    private void Start()
    {
        if(outline == null)
        {
            outline = GetComponent<Outline>();
        }
        StartCoroutine(LerpColor());
    }
   
    private IEnumerator LerpColor()
    {
        while (true)
        {
            outline.effectColor = Color.Lerp(from, to, Mathf.PingPong(Time.time, 1));
            yield return waitDuration;
        }
    }

}
