using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Shotgun : IWeapon
{   
    float modifier = 5f;

    public float GetWeaponAutoCooldown 
    {
        get;
        private set;
    }

    public Shotgun()
    {
        GetWeaponAutoCooldown = 0.35f;
    }

    public void Shoot(Vector3 playerPosition)
    {
        GameplayManager.instance.SpawnPlayerBullet(playerPosition);
        GameplayManager.instance.SpawnPlayerBullet(playerPosition, new Vector3(Random.Range(-modifier, modifier), 
                                                                            Random.Range(-modifier, modifier), 
                                                                                0));
        GameplayManager.instance.SpawnPlayerBullet(playerPosition, new Vector3(Random.Range(-modifier, modifier),
                                                                            Random.Range(-modifier, modifier),
                                                                                0));
        AudioCenter.instance.PlasmaShotSound();
    }
}
