using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput instance;
    public GameplayManager manager;
    public float speed = 0.5f;
    public Vector3 mouseTarget;    
    private IWeapon equippedWeapon;
    private float weaponCooldown;
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

    private void Start()
    {
        equippedWeapon = CurrentWeaponLoadout.instance.GetCurrentWeaponFromLoadout();
        weaponCooldown = equippedWeapon.GetWeaponAutoCooldown;
    }

    private void Update()
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

        mouseTarget = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));     

        if (Input.GetMouseButtonDown(0))
        {            
            equippedWeapon.Shoot(transform.position);
            weaponCooldown = equippedWeapon.GetWeaponAutoCooldown;
        }
        
        weaponCooldown -= Time.deltaTime;
        if(Input.GetMouseButton(0) && weaponCooldown < 0)
        {            
            equippedWeapon.Shoot(transform.position);
            weaponCooldown = equippedWeapon.GetWeaponAutoCooldown;
        }        
    }

}
