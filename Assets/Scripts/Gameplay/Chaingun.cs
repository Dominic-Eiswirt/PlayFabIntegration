using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Chaingun : IWeapon
{   
    float modifier = 2f;

    public float GetWeaponAutoCooldown 
    {
        get;
        private set;
    }

    public Chaingun()
    {
        GetWeaponAutoCooldown = 0.075f;
    }

    public void Shoot(Vector3 playerPosition)
    {
        //faster but more innacurate
        GameplayManager.instance.SpawnPlayerBullet(playerPosition, new Vector3(Random.Range(-modifier, modifier),
                                                                            Random.Range(-modifier, modifier),
                                                                                0));
        AudioCenter.instance.PlasmaShotSound();
    }
}
