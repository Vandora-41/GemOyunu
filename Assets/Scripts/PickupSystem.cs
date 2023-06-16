using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSystem : MonoBehaviour
{
    
    private bool isCoroutineRunning = false;

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Gem")){
            GameManager.Instance.gemCount--;
            GameManager.Instance.AddGem(other.GetComponent<Gem>()); 
            other.GetComponentInParent<Tile>().isSpawned = false;
            other.GetComponent<Gem>().ResetSize();
            other.gameObject.SetActive(false);
            this.GetComponent<Backpack>().AddCube(other.GetComponent<Gem>().id);
            
            
        }

        
   }

    private void OnTriggerStay(Collider other) {
        if(other.CompareTag("SaleArea")){
            if(!isCoroutineRunning)
            {
                StartCoroutine(RemoveItemsFromBackPack());
            }
        }
   }
    IEnumerator RemoveItemsFromBackPack()
    {
        isCoroutineRunning = true;

        float removeItemPerSec = 3 / 10; 

        for (int i = 0; i < 10; i++)
        {
            if (GameManager.Instance.gemBackpack.Count > 0)
            {
                GameManager.Instance.SetMoney();
                GameManager.Instance.gemBackpack.RemoveAt(GameManager.Instance.gemBackpack.Count - 1);
                this.GetComponent<Backpack>().RemoveCube();    
                yield return new WaitForSeconds(removeItemPerSec);
            }
            else
            {
                break;
            }
        }

        isCoroutineRunning = false;
    }
}
