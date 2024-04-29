using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    public Transform playerTransform; 

    public void GoToItem(itemData item)
    {
        playerTransform.position = item.goToPoint.position; 
        TryGettingItem(item);
    }
    private void TryGettingItem(itemData item){
        if(item.requiredItemID == -1 || GameManager.collectedItems.Contains(item.requiredItemID)){
            GameManager.collectedItems.Add(item.itemID);
        }
    }
}
