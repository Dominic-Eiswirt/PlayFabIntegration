using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class ObjectPooler
{
    private List<GameObject> objectList = new List<GameObject>();
    

    public void CreatePool(GameObject objectToCreate, int count)
    {
        int originalListCount = objectList.Count;
        for (int i = originalListCount; i < originalListCount + count ; i++)
        {

            objectList.Add(GameObject.Instantiate(objectToCreate));
            objectList[i].SetActive(false);
        }
    }

    //Spawn item gets the location of the enemy or player, or whever it should start.
    //Also give a optional target modifier (used for the shotgun to randomize the bullets)
    public void SpawnItem(Vector3 startPosition, Vector3 targetModifier = default(Vector3))
    {        
        bool spawnedItem = false;
        for (int i = 0; i < objectList.Count; i++)
        {
            if (!objectList[i].activeSelf)
            {
                spawnedItem = true;
                objectList[i].transform.position = startPosition;
                objectList[i].SetActive(true);
                objectList[i].GetComponent<IBullet>().TargetModifier((Vector3)targetModifier);
                objectList[i].GetComponent<IBullet>().Init();
                break;
            }
        }

        //all our game objs are currently alive and active, we need to expand the pool
        if (!spawnedItem && objectList.Count > 0) 
        {
            CreatePool(objectList[0], 3);
            //try again
            SpawnItem(startPosition);
        }
    }
    public void NukePool()
    {
        foreach(GameObject o in objectList)
        {
            GameObject.Destroy(o);
        }
    }
}
