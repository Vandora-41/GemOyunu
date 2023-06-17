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
    public Gem[] gemList;
    public int gemCount;
    public int maxGem;
    public TMP_Text paraText;
    public List<Gem> gemBackpack = new List<Gem>();

    public TMP_Text[] gemText;

    private float currentMoney = 0;


    private void Awake() {
        Instance = this;

        if(!PlayerPrefs.HasKey("GreenGem")){
            PlayerPrefs.SetInt("GreenGem", 0);
            PlayerPrefs.SetInt("PurpleGem", 0);
            PlayerPrefs.SetInt("YellowGem", 0);
            PlayerPrefs.SetFloat("Cash",0);
        }
        paraText.text = PlayerPrefs.GetFloat("Cash").ToString("F0");
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
    public void SetGemTexts(){
        for (int i = 0; i < gemList.Length; i++)
        {
            gemText[i].text =PlayerPrefs.GetInt(gemList[i].gemName).ToString();
        }
    }
    public void AddGem(Gem gem){
        gemBackpack.Add(gem);
        PlayerPrefs.SetInt(gem.gemName, PlayerPrefs.GetInt(gem.gemName)+1);
        gemText[gem.id].text = PlayerPrefs.GetInt(gem.gemName).ToString();
        PlayerPrefs.Save();

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
        PlayerPrefs.SetFloat("Cash",PlayerPrefs.GetFloat("Cash") + currentMoney);
        currentMoney = 0;
        paraText.text = PlayerPrefs.GetFloat("Cash").ToString("F0");
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