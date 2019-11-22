﻿using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput instance;
    public GameplayManager manager;
    public float speed = 0.5f;
    public Vector3 mouseTarget;
    float modifier = 40;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    void Update()
    {
        if(Input.GetKey(KeyCode.W) && this.transform.position.y < 60)
        {
            this.transform.position += Vector3.up * speed;
        }
        if (Input.GetKey(KeyCode.A) && this.transform.position.x > -114)
        {
            this.transform.position += -Vector3.right * speed;
        }
        if (Input.GetKey(KeyCode.S) && this.transform.position.y > -60)
        {
            this.transform.position -= Vector3.up * speed;
        }
        if (Input.GetKey(KeyCode.D) && this.transform.position.x < 114)
        {
            this.transform.position += Vector3.right * speed;
        }

        if (Input.GetMouseButtonDown(0))
        {            
            mouseTarget = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));            
            manager.SpawnPlayerBullet(PlayerInput.instance.gameObject.transform.position);
            if (WeaponsSelector.instance.selectedWeapon == WeaponSelectedEnum.Shotgun)
            {

                GameplayManager.instance.SpawnPlayerBullet(this.transform.position, new Vector3(Random.Range(-modifier, modifier),
                                                                                                  Random.Range(-modifier, modifier),
                                                                                                        0));
                GameplayManager.instance.SpawnPlayerBullet(this.transform.position, new Vector3(Random.Range(-modifier, modifier),
                                                                                                    Random.Range(-modifier, modifier),
                                                                                                        0));
            }
        }
    }
}
