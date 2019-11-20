using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "WeaponCard", menuName = "WeaponCard", order = 1)]
public class WeaponCard : ScriptableObject
{
    public Sprite cardImage;    
    public int progress;
    public int levelup;
    public int ID;
}
