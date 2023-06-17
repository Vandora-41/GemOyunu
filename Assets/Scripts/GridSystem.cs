using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class GridSystem : MonoBehaviour
{
    public int width;
    const int height = 1; //Degistirilmemesi lazım
    public int depth; 
    public float distance = 2f;
    private int oldDepth,oldHeight,oldWidth;
    public GameObject gridPrefab;
    private GameObject[,,] gridArray;

    private void Start()
    {
       GameManager.Instance.gridList.Add(this);
       oldWidth = width;
       oldDepth = depth;
       oldHeight = height;
    }

    public void GenerateGrid()
    {   
        if(Application.isPlaying){
            if (gridArray != null)
            {
                GameManager.Instance.DestroyTiles();
            }
        }
        else{
            if (gridArray != null)
            {
                DestroyGrid();
            }
        }
        

        oldWidth = width;
        oldDepth = depth;
        oldHeight = height;
        gridArray = new GameObject[width, height, depth];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                for (int z = 0; z < depth; z++)
                {
                    
                    GameObject newGridObj = Instantiate(gridPrefab, new Vector3(transform.position.x + x * distance, transform.position.y -.50f+ y * distance, transform.position.z + z * distance), Quaternion.identity);
                    newGridObj.transform.parent = this.transform;
                    gridArray[x, y, z] = newGridObj;
                }
            }
        }
    }
    public void DestroyGrid(){
        if (gridArray != null)
        {
            for (int x = 0; x < oldWidth; x++)
                {
                    for (int y = 0; y < oldHeight; y++)
                    {
                        for (int z = 0; z < oldDepth; z++)
                        {
                            DestroyImmediate(gridArray[x, y, z]);
                        }
                    }
                }
            oldWidth = width;
            oldDepth = depth;
            oldHeight = height;
        }
    }
}


#if UNITY_EDITOR
[CustomEditor(typeof(GridSystem))]
public class GridSystemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GridSystem myScript = (GridSystem)target;
        if(GUILayout.Button("Create Grid"))
        {
            myScript.GenerateGrid();
        }
        if(GUILayout.Button("Destroy Grid"))
        {
            if(Application.isPlaying){
                GameManager.Instance.DestroyTiles();
            }
            else{
                myScript.DestroyGrid();
            }
            
        }
    }
}
#endif