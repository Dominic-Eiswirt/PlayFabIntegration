using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public WeaponCard weaponCardReference;
    public Text weaponPriceText;    
    public Text headText;
    [SerializeField] private Image background;

    private void Awake()
    {
        background.sprite = weaponCardReference.cardImage;
        weaponPriceText.text = "";
    }

}

