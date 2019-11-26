using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InventorySpawner : MonoBehaviour
{
    [Range(2, 15)]
    [SerializeField] private int gridSize;        
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

        //Set grid size dependant on the size of the spawn list. Needs refining                         
        if (useDynamicGrid && spawnList.Count > 3 && spawnList.Count <= 5)
        {
            gridSize = (int)((spawnList.Count) / 2);
        }
        if(useDynamicGrid && spawnList.Count > 5)
        { 
            gridSize = (int)((spawnList.Count) / 3);
        }
        if(useDynamicGrid && spawnList.Count > 15)
        {
            gridSize = (int)((spawnList.Count) / 4);
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
