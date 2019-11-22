
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerBullet : Bullet
{    
    private float speed = 45f;
    private Vector3 bulletDirection;    
    void Update()
    {
        this.transform.position += bulletDirection * speed * Time.deltaTime;
        base.Update();
    }
    public override void Init()
    {   
        
        bulletDirection = ((PlayerInput.instance.mouseTarget + modifiedTarget) - this.transform.position).normalized;
        
       
    }


}
