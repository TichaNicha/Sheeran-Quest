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
        if (item != null){
            // update dialogue box
            gameManager.UpdateDialogueBox(null);
            playerWalking = true;        
            // move player
            StartCoroutine(gameManager.MoveToPoint(playerTransform, item.goToPoint.position));
            // equipment stuff
            TryGettingItem(item);
        } 
    }


    private void TryGettingItem(itemData item){
        bool canGetItem = item.requiredItemID == -1 || (GameManager.collectedItems.Contains(item.requiredItemID) || GameManager.collectedItems.Contains(item.requiredItemID2));
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
            if (item.itemID == 4){
             gameManager.CheckWinConditions();
            }

        } else{
            gameManager.UpdateDialogueBox(item);
        }
        yield return null; // do it over the course of multiple frames
    }
}