using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoringSystem : MonoBehaviour
{
    public PlayerData playerData;
    private TMP_Text text;

    private void Start(){
        text = GetComponent<TMP_Text>();
    }
    private void Update(){
        text.text = ""+playerData.score;
    }

    
}
