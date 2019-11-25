using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthDisplay : MonoBehaviour
{

    public Image healthGreen;
    float initialHealth;
    void Start()
    {
        GameplayManager.instance.OnDamageEvent += AdjustHealthDisplay;
        initialHealth = GameplayManager.instance.playerHealth;
    }

    private void AdjustHealthDisplay()
    {        
        healthGreen.fillAmount -= 1f/initialHealth;
    }
}
