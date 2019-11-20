using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponCard weaponCardReference;
    [SerializeField] private Image background;
    [SerializeField] private Text progressBar;
    
    private void Start()
    {
        background.sprite = weaponCardReference.cardImage;        
        progressBar.text = string.Format("{0} / {1}", weaponCardReference.progress, weaponCardReference.levelup);
    }

}

