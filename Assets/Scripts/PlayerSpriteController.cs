
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerSpriteController : MonoBehaviour
{    
    private const float originalTimer = 0.5f;

    [SerializeField] private Sprite[] neutralHealthySprites;
    [SerializeField] private Sprite[] neutralDamagedSprites;    
    [SerializeField] private Sprite[] damagedSprites;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private float timer = originalTimer;
    private float damageTakenTimer = 0;
    private short damageTaken;

    void Start()
    {
        GetComponentInChildren<SpriteRenderer>();
        GameplayManager.instance.OnDamageEvent += DamageWasTaken;
    }

    void Update()
    {        
        timer -= Time.deltaTime;
        damageTakenTimer -=  Time.deltaTime;
        if(timer < 0 && damageTakenTimer < 0)
        {            
            if(damageTaken < 2)
            { 
                spriteRenderer.sprite = neutralHealthySprites[Random.Range(0, neutralHealthySprites.Length)];
            }
            else
            {
                spriteRenderer.sprite = neutralDamagedSprites[Random.Range(0, neutralDamagedSprites.Length)];
            }
            timer = originalTimer;
        }
    }

    public void DamageWasTaken()
    {
        damageTakenTimer =  originalTimer;
        damageTaken++;
        spriteRenderer.sprite = damagedSprites[Random.Range(0, damagedSprites.Length)];
    }
}
