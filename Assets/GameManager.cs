using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static List<int> collectedItems = new List<int>();
    public static float moveSpeed = 3.5f, moveAccuracy = 0.15f;

    
    public IEnumerator MoveToPoint(Transform myObject, Vector2 point){        
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
        yield return null; // do it over the course of multiple frames
    }
}
