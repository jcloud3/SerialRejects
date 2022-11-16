using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    Animator anim;
    public Transform player;
    State currentState;
    // Start is called before the first frame update
    void Start()
    {
        
        anim = this.GetComponentInChildren(typeof(Animator)) as Animator;
        currentState = new Idle(this.gameObject,anim,player);
    }

    // Update is called once per frame
    void Update()
    {
        currentState = currentState.Process();
        
    }
}
