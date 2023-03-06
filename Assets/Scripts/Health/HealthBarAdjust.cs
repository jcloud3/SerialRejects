using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarAdjust : MonoBehaviour
{
    public Slider _healthSlider;


    private void Start()
    {
         _healthSlider = this.GetComponent<Slider>();
    }
    public void SetMaxHealth(int maxHealth){
        _healthSlider.maxValue = maxHealth;
        _healthSlider.value = maxHealth;
    }

    public void SetHealth(int health){
        _healthSlider.value = health;
    }
}
