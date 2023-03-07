using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager gameManager{get; private set; }
    //probably need to convert all these into arrays to handle multiple players.
    public UnitHealth _playerHealth = new UnitHealth(100,100);
    //public UnitHealth _player2Health = new UnitHealth(100,100);
    //public ScoringSystem _playerScore = new ScoringSystem(0);
    //this can keep track of the number of players in the game and assign new players a number for purposes of tracking stats
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
    }
    public int GetNumPlayers(){
        return numPlayers;
    }
    public void SetNumPlayers(int currentNumPlayers){
        numPlayers = currentNumPlayers;
    }

   
}
