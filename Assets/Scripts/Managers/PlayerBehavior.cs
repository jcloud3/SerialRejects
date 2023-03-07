using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{

    [SerializeField] HealthBarAdjust _healthBar;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //call this to damage player
    private void PlayerTakeDamage(int damage){
        //GameManager.gameManager._playerHealth.DamageUnit(damage);
        //_healthBar.SetHealth(GameManager.gameManager._playerHealth.Health);
    }
    //call this to heal player
    private void PlayerHeal(int healAmount){
        //GameManager.gameManager._playerHealth.HealUnit(healAmount);
        //_healthBar.SetHealth(GameManager.gameManager._playerHealth.Health);
    }
}
