using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public WeaponCard weaponCardReference;
    public Text progressBar;    
    [SerializeField] private Image background;

    private void Awake()
    {
        background.sprite = weaponCardReference.cardImage;        
        progressBar.text = string.Format("{0} / {1}", weaponCardReference.progress, weaponCardReference.levelup);
    }

}

