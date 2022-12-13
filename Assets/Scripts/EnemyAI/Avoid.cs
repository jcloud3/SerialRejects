using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avoid : State
{
    //can use this to track which player is being pursued
   int currentPlayer = -1;
   public Avoid(GameObject _npc, Animator _anim, Transform _player):base(_npc,_anim,_player)
    {
        name = STATE.AVOID;
        //controls = npc.GetComponent<EnemyMove>();
        //set speed of npc
        //agent.isStopped = false
    }
    public override void Enter()
    {
        
        currentPlayer = 0;
        anim.SetTrigger("isWalking");
        base.Enter();
    }
    public override void Update()
    {
        /*if (Random.Range(1,100)<2){
            controls.SetJump();
        }*/
        
        controls.MoveAway();
        /*if (CanAttack()){
            nextState = new Attack(npc,anim,player);
            stage = EVENT.EXIT;
        }*/
        //this probably makes no sense, need to rethink this once AI complete
        if (controls.GetDistanceToTarget()>8.0f){
            nextState = new Idle(npc,anim,player);
            stage = EVENT.EXIT;
        }
        
        
    }
    public override void Exit()
    {
        anim.ResetTrigger("isWalking");
        base.Exit();
    }
    
}
