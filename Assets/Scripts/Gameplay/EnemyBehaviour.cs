using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

//Desired behaviour - get player pos once, move to that position while shooting at the actual position of the player
public class EnemyBehaviour : MonoBehaviour
{
    private Vector3 target;
    [SerializeField] private int health = 3;
    private float movementSpeed = 10;
    private const float originalFireRate = 1f;
    private float fireRate = originalFireRate;
    void Start()
    {
        target = (PlayerInput.instance.gameObject.transform.position - this.transform.position).normalized;
    }

    private void Update()
    {        
        fireRate -= Time.deltaTime;
        if(fireRate < 0)
        {            
            GameplayManager.instance.SpawnEnemyBullet(this.transform.position);
            fireRate = originalFireRate;
        }
        this.transform.position += target * movementSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {        
        //Player bullet is a trigger
        health--;        
        other.gameObject.SetActive(false);
        if(health <= 0)
        {
            GameplayManager.instance.coreGameData.score++;
            Destroy(this.gameObject);
        }
    }
}
