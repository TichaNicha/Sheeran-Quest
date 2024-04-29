using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemData : MonoBehaviour
{
    public int itemID, requiredItemID;
    public int requiredItemID2 = -1;

    public Transform goToPoint;
    public GameObject[] objectsToRemove;
    public string objectName;
    public Vector2 nameTagSize = new Vector2(2.5f, 0.5f);

    [TextArea(3,3)]
    public string dialogueMessage;
    public Vector2 dialogueBoxSize = new Vector2(3.5f, 0.5f);
}
