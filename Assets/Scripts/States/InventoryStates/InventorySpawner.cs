using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InventorySpawner : MonoBehaviour
{
    [Range(2, 15)]
    [SerializeField] private int gridSize;
    [SerializeField] private GameObject Pistol;
    [SerializeField] private GameObject Shotgun;    
    private GridLayoutGroup gridLayout;
    private PlayerInventory playerInventory;
    public bool useDynamicGrid = true; //Grid that depends on the amount of the object we are instantiating
    public GameObject loadingText;
    public Button selectionButtonPrefab;
    private void OnEnable()
    {
        loadingText.SetActive(true);
    }
    public void InstantiateCells()
    {
        //Get references from self
        playerInventory = GetComponent<PlayerInventory>();
        gridLayout = GetComponent<GridLayoutGroup>();
        List<GameObject> spawnList = playerInventory.GetSpawnList();
        gridSize = 2;

        //Set grid size dependant on the size of the spawn list 
        //Higher spawn list gets higher division, because the spawnlist count would cause the grid to be very high, which means we don't utilize the grid to its
        //potential (example with 20 objects: 2 rows of 10 objects vs 4 rows of 5 objects). 
        //Needs a better implementation though. 20 objects will give us 3 rows of 6 objects, 4th row is incomplete. Instead of 4 complete rows of 5 objects.        
        if (useDynamicGrid && spawnList.Count > 3 && spawnList.Count <= 8)
        {
            gridSize = (int)((spawnList.Count) / 2);
        }
        else if(useDynamicGrid && spawnList.Count > 8)
        { 
            gridSize = (int)((spawnList.Count) / 3);
        }
        gridLayout.constraintCount = gridSize;
        float spriteSize = 300f / (gridSize / 2f);
        gridLayout.cellSize = new Vector2(spriteSize, spriteSize);
        Text childText = null;
        int i = 0;
        
        GameObject newGo;
        Button newButton;
        foreach(GameObject o in spawnList)
        {
            newGo = Instantiate(o, this.transform);
            childText = newGo.GetComponentInChildren<Text>();
            newButton = Instantiate(selectionButtonPrefab, newGo.transform);
            newButton.GetComponent<WeaponInventorySelection>().myType = newGo.GetComponent<Weapon>().myType;
            childText.fontSize = (int)spriteSize / 5;
            i++;
        }
        //After we finish instantiating and parenting all the gameobjects to the gridlayout parent, we move the parent itself
        float width = Screen.width;
        float height = Screen.height;
        float whRatio = (width / height);
        Vector3 center = new Vector3(width / 2, height / 2, 0);
        float xOffsetModifier = 1.2f;
        float yOffsetModifier = 1.2f;        
        this.transform.position = center;
        this.transform.position += new Vector3(gridSize * -spriteSize / whRatio / xOffsetModifier,   //x
                                                gridSize * spriteSize / whRatio / yOffsetModifier,  //y
                                                0);                                                //z
        loadingText.SetActive(false);
    }

}
