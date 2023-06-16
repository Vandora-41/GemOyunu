using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject[] gems;

    public bool isSpawned = false;

    
    void Start()
    {
        GameManager.Instance.tileList.Add(this);
    }

    public void SpawnGem(){
        float randomNumber = Random.value;
        if (randomNumber < 0.5f) {
            gems[0].SetActive(true);
            gems[0].GetComponent<Gem>().StartGem();
        }
        else if (randomNumber < 0.85f) {
            gems[1].SetActive(true);
            gems[1].GetComponent<Gem>().StartGem();
        } 
        else {
           gems[2].SetActive(true);
           gems[2].GetComponent<Gem>().StartGem();
        }
        isSpawned = true;
    }


}
