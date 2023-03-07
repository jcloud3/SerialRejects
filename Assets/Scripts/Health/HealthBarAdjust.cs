using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarAdjust : MonoBehaviour
{
    public Slider _healthSlider;
    public PlayerData playerInfo;

    private void Start()
    {
         _healthSlider = this.GetComponent<Slider>();
    }
    private void Update(){
        _healthSlider.maxValue = playerInfo.maxHealth;
        _healthSlider.value = playerInfo.health;
    }
    
}
