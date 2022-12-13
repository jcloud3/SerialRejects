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
        currentState = new Idle(this.gameObject,anim,player[0].transform);
    }

    // Update is called once per frame
    void Update()
    {
        currentState = currentState.Process();
        
    }
}
