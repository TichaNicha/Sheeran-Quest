using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static List<int> collectedItems = new List<int>();
    public static float moveSpeed = 3.5f, moveAccuracy = 0.15f;
    public RectTransform nameTag, dialogueBox;
    public GameObject nameTagObject;

    public Animator playerAnimator;
    public GameObject[] localScenes;
    public int activeLocalScene;

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

    // player has to have picked up kat sheeran(item ID 4)
    public void CheckWinConditions(){
    // after picking up kat sheeran, check inventory 
        foreach(int i in collectedItems){
            if (i == 2){
                ChangeScene(1);// lose screen
                return;
            } else{
                ChangeScene(2);// win screen
                return;
            }
        }       
    }

    public void ChangeScene(int sceneNumber){
        localScenes[sceneNumber].SetActive(true);
    }
    

    public void UpdateNameTag(itemData item){
        // change name
        nameTag.GetComponentInChildren<TextMeshProUGUI>().text = item.objectName;
        // change nametag size
        nameTag.sizeDelta = item.nameTagSize;
        // move tag
        nameTag.localPosition= new Vector2(item.nameTagSize.x/2, -0.5f);
    }

    public void UpdateDialogueBox(itemData item){
        if(item == null){
            dialogueBox.gameObject.SetActive(false);
            return;
        } else{
            dialogueBox.gameObject.SetActive(true);
            // change name
            dialogueBox.GetComponentInChildren<TextMeshProUGUI>().text = item.dialogueMessage;
            // change nametag size
            dialogueBox.sizeDelta = item.dialogueBoxSize;
            // move tag
            dialogueBox.parent.localPosition= new Vector2(0, 0);  
        }
      
    }    
}
