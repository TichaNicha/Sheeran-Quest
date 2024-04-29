using System.Threading;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    public bool playerWalking;
    public Transform playerTransform; 
    GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void GoToItem(itemData item)
    {
        StartCoroutine(gameManager.MoveToPoint(playerTransform, item.goToPoint.position));
        playerWalking = true;
        TryGettingItem(item);
    }


    private void TryGettingItem(itemData item){
        bool canGetItem = item.requiredItemID == -1 || GameManager.collectedItems.Contains(item.requiredItemID);
        if(canGetItem){
            GameManager.collectedItems.Add(item.itemID);
        }
        StartCoroutine(UpdateSceneAfterAction(item, canGetItem));
    }

    private IEnumerator UpdateSceneAfterAction(itemData item, bool canGetItem){
        while(playerWalking){
            yield return new WaitForSeconds(0.05f);
        }
        if(canGetItem){
            // remove everything in array
            foreach(GameObject g in item.objectsToRemove){
                Destroy(g);
            }       
            UnityEngine.Debug.Log("Item picked up");
            gameManager.nameTagObject.SetActive(false);
        }
        yield return null; // do it over the course of multiple frames
    }
}