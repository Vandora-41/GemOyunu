using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backpack : MonoBehaviour
{    
    public GameObject[] cubePrefab;
    public Transform backpack;
    public Transform[] stackPos;

    private List<GameObject> cubeList = new List<GameObject>();
    private int totalCubes = 0;
    private float currentstack = 0.1f;

   public void AddCube(int x)
    {
        if(totalCubes == 4){
            currentstack = currentstack + 0.1f;
            totalCubes=0;

        }
        
        Vector3 spawnPos = new Vector3(stackPos[totalCubes].position.x,stackPos[totalCubes].position.y + currentstack,stackPos[totalCubes].position.z);
        
        GameObject newCube = Instantiate(cubePrefab[x], spawnPos, stackPos[totalCubes].rotation,backpack);
        cubeList.Add(newCube);

        totalCubes++;
    }

    public void RemoveCube()
    {
        if(cubeList.Count > 0)
        {   
            GameObject lastCube = cubeList[cubeList.Count - 1];
            Destroy(lastCube);
            cubeList.RemoveAt(cubeList.Count - 1);
            
            totalCubes--;
            if(totalCubes == 0){
                totalCubes = 4;
                currentstack = currentstack - 0.1f;
            }
        }
    }
}
