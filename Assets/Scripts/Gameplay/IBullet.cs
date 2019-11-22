using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public interface IBullet
{
    void TargetModifier(Vector3 targetPos);
    void Init(); 
    
}
