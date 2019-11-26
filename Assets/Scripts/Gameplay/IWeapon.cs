using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public interface IWeapon
{ 
    void Shoot(Vector3 playerPosition);
    float GetWeaponAutoCooldown { get; }
}
