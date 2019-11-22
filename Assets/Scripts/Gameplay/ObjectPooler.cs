using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class ObjectPooler
{
    private List<GameObject> objectList = new List<GameObject>();    
    //Queue<GameObject> objectQueue = new Queue<GameObject>();
   
    public void CreatePool(GameObject objectToCreate, int count)
    {
        int originalListCount = objectList.Count;
        for (int i = originalListCount; i < originalListCount + count ; i++)
        {

            objectList.Add(GameObject.Instantiate(objectToCreate));
            objectList[i].SetActive(false);
        }
    }

    public void SpawnItem(Vector3 position)
    {
        bool spawnedItem = false;
        for(int i = 0; i < objectList.Count; i++)
        {
            if(!objectList[i].activeSelf)
            {
                spawnedItem = true;
                objectList[i].transform.position = position;
                objectList[i].SetActive(true);
                objectList[i].GetComponent<IBullet>().Init();
                break;
            }
        }
        if(!spawnedItem && objectList.Count > 0) //all our game objs are currently alive and active, we need more
        {
            CreatePool(objectList[0], 3);
            //try again
            SpawnItem(position);
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
