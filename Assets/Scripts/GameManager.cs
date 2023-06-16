using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public List<GridSystem> gridList = new List<GridSystem>();
    public List<Tile> tileList = new List<Tile>();
    public int gemCount;
    public int maxGem;
    public TMP_Text paraText;
    public List<Gem> gemBackpack = new List<Gem>();

    public float currentMoney = 0;


    private void Awake() {
        Instance = this;
    }

    private void Update() {
        GemCheck();
    }

    private void GemCheck(){
        if(gemCount < maxGem){
            Tile randomTile = PickRandomUnspawnedTile();

                if (randomTile != null)
                {
                    randomTile.SpawnGem();
                }
        }

    }
    public void AddGem(Gem gem){
        gemBackpack.Add(gem);
    }
    public Tile PickRandomUnspawnedTile()
    {
        var unspawnedTiles = tileList.Where(tile => !tile.isSpawned).ToList();

        if (unspawnedTiles.Count == 0)
        {
            return null; 
        }
        
        int randomIndex = UnityEngine.Random.Range(0, unspawnedTiles.Count);
        gemCount++;
        return unspawnedTiles[randomIndex];
    }

    public void SetMoney(){
        currentMoney += gemBackpack[gemBackpack.Count-1].salePrice * gemBackpack[gemBackpack.Count-1].currentScale;   
        paraText.text = currentMoney.ToString("F0");
    }


    public void DestroyTiles(){
        if(Application.isPlaying){
            for (int i = 0; i < tileList.Count; i++)
            {
                Destroy(tileList[i].gameObject);     
            }
            tileList.Clear();
            gemCount = 0;
        }
        else{
            Debug.Log("Oyun baslamadığı icin Yok etme işlemini GridSystem üzerinden yapınız.");
        }
    }

}
[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GameManager myScript = (GameManager)target;
        if(GUILayout.Button("Destroy Tiles"))
        {
            myScript.DestroyTiles();
        }
    }
}