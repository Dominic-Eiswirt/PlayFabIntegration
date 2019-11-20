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

    private void OnEnable()
    {
        loadingText.SetActive(true);
    }
    public void InstantiateCells()
    {
        
        //Check child count - we don't want duplicate instantiation
        // if(this.transform.childCount == 0)
        // {
        playerInventory = GetComponent<PlayerInventory>();
            gridLayout = GetComponent<GridLayoutGroup>();
            List<GameObject> spawnList = playerInventory.GetSpawnList();
            if(useDynamicGrid && spawnList.Count > 4)
            { 
                gridSize = (int)((spawnList.Count+1) / 2);
            }
            gridLayout.constraintCount = gridSize;
            float spriteSize = 300f / (gridSize / 2f);
            gridLayout.cellSize = new Vector2(spriteSize, spriteSize);
            Text childText = null;
            int i = 0;
            Debug.Log("Spawnlist count " + spawnList.Count);
            foreach(GameObject o in spawnList)
            {
                childText = Instantiate(o, this.transform).GetComponentInChildren<Text>();
                childText.fontSize = (int)spriteSize / 5;
                i++;
            }
            for (int y = gridSize; y > 0; y--)
            {
                for (int x = 0; x < gridSize; x++)
                {
                    
                    //if (playerInventory.weapons.pistolCount > 0)
                    //{
                    //    playerInventory.weapons.pistolCount--;
                    //    childText = Instantiate(Pistol, this.transform).GetComponentInChildren<Text>();
                    //    childText.fontSize = (int)spriteSize / 5;
                    //
                    //
                    //}
                    //else if (playerInventory.weapons.shotgunCount > 0)
                    //{
                    //    playerInventory.weapons.shotgunCount--;
                    //    childText = Instantiate(Shotgun, this.transform).GetComponentInChildren<Text>();
                    //    childText.fontSize = (int)spriteSize / 5;
                    //}
                }
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
        // }
        // else
        // {            
        //     //Turn on if we have already instantiated - Technically this isn't even needed because our inventory state disables this parent game object, instead of the children. 
        //     //But let's keep this for now we can deleted it later
        //     for(int i = 0; i < transform.childCount; i++)
        //     {
        //         transform.GetChild(i).gameObject.SetActive(true);                
        //     }
        // }
    }

}
