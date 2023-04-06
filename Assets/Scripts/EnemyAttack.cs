using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private void OnTriggerEnter2d(Collider2D other){
        Debug.Log("hit something");
        
        if (other.CompareTag("Player")){
            Debug.Log("hit player");
        }
    }
    private void OnTriggerStay2d(Collider2D other){
         Debug.Log("triggerstay");
        
        if (other.CompareTag("Player")){
            Debug.Log("hit player");
        }
    }
}
