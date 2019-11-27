using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EnemyBullet : Bullet
{       
    private float speed = 25f;
    private Vector3 bulletDirection;

    void Update()
    {        
        this.transform.position += bulletDirection * speed * Time.deltaTime;
        base.Update();
    }

    void OnCollisionEnter(Collision other)
    {
        GameplayManager.instance.RegisterHit();
        gameObject.SetActive(false);
    }

    public override void Init()
    {
        bulletDirection = ((PlayerInput.instance.gameObject.transform.position + modifiedTarget) - this.transform.position).normalized;
    }


    
}
