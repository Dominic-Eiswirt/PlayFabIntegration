using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Pistol : IWeapon
{
    public float GetWeaponAutoCooldown
    {
        get;
        private set;        
    }
    public Pistol()
    {
        GetWeaponAutoCooldown = 0.25f;
    }
    public void Shoot(Vector3 playerPosition)
    {
        GameplayManager.instance.SpawnPlayerBullet(playerPosition);
        AudioCenter.instance.PlasmaShotSound();
    }
}
