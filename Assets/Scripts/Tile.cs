using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Tile : MonoBehaviour
{
    public Gem[] gems;
    public bool isSpawned = false;

    void Start()
    {
        GameManager.Instance.tileList.Add(this);
    }

    public void SpawnGem()
    {
        float totalSpawnChance = 0;
        foreach (Gem gem in gems)
        {
            totalSpawnChance += gem.spawnChance;
        }

        float randomNumber = Random.value * totalSpawnChance;
        float birikmis = 0;

        foreach (Gem gem in gems)
        {
            birikmis += gem.spawnChance;
            if (randomNumber <= birikmis)
            {
                gem.gameObject.SetActive(true);
                gem.StartGem();
                isSpawned = true;
                break;
            }
        }
    }   
}
