using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{

    public static GameManager gameManager{get; private set; }
    //probably need to convert all these into arrays to handle multiple players.
    public GameObject trix;
    //public UnitHealth _player2Health = new UnitHealth(100,100);
    //public ScoringSystem _playerScore = new ScoringSystem(0);
    //this can keep track of the number of players in the game and assign new players a number for purposes of tracking stats
    public int numPlayers = 0;
    public void OnSpawn(InputAction.CallbackContext context){
        
        //jump = context.ReadValue<float>()>0;
        SpawnPlayer("trix");
        Debug.Log("Spawn");
        
    }

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
       trix.SetActive(false);
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
        }

    }

   
}
