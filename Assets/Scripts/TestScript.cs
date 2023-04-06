using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2d(Collider2D other){
        Debug.Log("hit something");
        
        if (other.CompareTag("Enemy")){
            Debug.Log("hit player");
        }
    }
    private void OnTriggerStay2d(Collider2D other){
         Debug.Log("triggerstay");
        
        if (other.CompareTag("Enemy")){
            Debug.Log("hit player");
        }
    }
    private void OnCollisionEnter2d(Collider2D other){
        Debug.Log("Collider");
    }
}
