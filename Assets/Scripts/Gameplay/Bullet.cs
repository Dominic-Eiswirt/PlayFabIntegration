using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public abstract class Bullet : MonoBehaviour, IBullet
{
    protected void Update()
    {
        OutOfBoundsControl();
    }
    protected void OutOfBoundsControl()
    {
        if (this.transform.position.x < -114 || this.transform.position.x > 114 || this.transform.position.y < -66 || this.transform.position.y > 66)
        {
            gameObject.SetActive(false);
        }
    }

    public abstract void Init();
}
    
