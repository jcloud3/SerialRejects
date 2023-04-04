using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{

    public static GameManager gameManager{get; private set; }
    
    public GameObject trix;
     
    public int numPlayers = 0;
    

    // Start is called before the first frame update
    void Awake()
    {
        if (gameManager != null && gameManager != this){
            Destroy(this);

        }
        else{
            gameManager = this;
        }
       //players = GameObject.FindGameObjectsWithTag("Player");
       trix = GameObject.Find("Trix");
       //trix.SetActive(false);
       Debug.Log("Spawn");
    }
    
    public int GetNumPlayers(){
        return numPlayers;
    }
    public void SetNumPlayers(int currentNumPlayers){
        numPlayers = currentNumPlayers;
    }
    public void SpawnPlayer(string PlayerName){
        if (PlayerName == "trix"){
            trix.SetActive(true);
            trix.GetComponent<Player>().Spawn();
            trix.GetComponentInChildren<SpriteRenderer>().enabled = true;
        }
    }

    public void KillPlayer(string PlayerName){
        if (PlayerName == "trix"){
            trix.SetActive(false);
            //trix.GetComponent<Player>().Spawn();
        }
    }

   
}
