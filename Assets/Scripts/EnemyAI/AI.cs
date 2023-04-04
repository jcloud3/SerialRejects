using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    Animator anim;
    public GameObject[] player;
    State currentState;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player");
        anim = this.GetComponentInChildren(typeof(Animator)) as Animator;
        if(player.Length>0){
            currentState = new Idle(this.gameObject,anim,player[0].transform);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.Length==0){
            player = GameObject.FindGameObjectsWithTag("Player");
        }
        if (player.Length>0 && currentState==null){
            currentState = new Idle(this.gameObject,anim,player[0].transform);
        }
        if(currentState!=null){
            currentState = currentState.Process();
        }
        
        
    }
}
