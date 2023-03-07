using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerData playerData;
    //logic for damage, lives, score should all be handled here
    void Update()
    {
        playerData.currentPosition = transform.position;
    }


    public void DamageUnit(int damageAmount){
        if (playerData.health>0){
            playerData.health -= damageAmount;
        }
    }

    public void HealUnit(int healAmount){
        if (playerData.health < playerData.maxHealth){
            playerData.health += healAmount;
        }
        if (playerData.health > playerData.maxHealth){
            playerData.health = playerData.maxHealth;
        }
    }

    public void IncreaseScore(int increaseAmount){
        
            playerData.score += increaseAmount;
        
        

    }
    public void DecreaseScore(int decreaseAmount){
        if (playerData.score>0){
            playerData.score -= decreaseAmount;
        }
        if (playerData.score < 0){
            playerData.score = 0;
        }
    }
}
