using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static List<int> collectedItems = new List<int>();
    public static float moveSpeed = 3.5f, moveAccuracy = 0.15f;
    public RectTransform nameTag;
    public GameObject nameTagObject;

    public Animator playerAnimator;

    void Start()
    {
        playerAnimator.SetBool("isWalking", false);
    }
    
    public IEnumerator MoveToPoint(Transform myObject, Vector2 point){  

        playerAnimator.SetBool("isWalking", true);

        // set direction
        Vector2 positionDifference = point - (Vector2)myObject.position;
        while(positionDifference.magnitude>moveAccuracy){        
            // move in direction frame by frame        
            myObject.Translate(moveSpeed*positionDifference.normalized * Time.deltaTime);
            positionDifference = point - (Vector2)myObject.position;
            yield return null; // do it over the course of multiple frames
        }
        myObject.position = point;
        // set player walking to false
        if(myObject == FindObjectOfType<ClickManager>().playerTransform){
            FindObjectOfType<ClickManager>().playerWalking = false;
        }

        playerAnimator.SetBool("isWalking", false);

        yield return null; // do it over the course of multiple frames
    }


    public void UpdateNameTag(itemData item){
        // change name
        nameTag.GetComponentInChildren<TextMeshProUGUI>().text = item.objectName;
        // change nametag size
        nameTag.sizeDelta = item.nameTagSize;
        // move tag
        nameTag.localPosition= new Vector2(item.nameTagSize.x/2, -0.5f);
    }
}
