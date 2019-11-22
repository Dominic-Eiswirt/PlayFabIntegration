using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public abstract class Bullet : MonoBehaviour, IBullet
{
    protected Vector3 modifiedTarget = Vector3.zero;
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

    public void TargetModifier(Vector3 targetPos)
    {
        modifiedTarget = targetPos;        
    }
}
    
