using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursue : State
{
    //can use this to track which player is being pursued
   int currentPlayer = -1;
   public Pursue(GameObject _npc, Animator _anim, Transform _player):base(_npc,_anim,_player)
    {
        name = STATE.PURSUE;
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
        
        controls.Move();
        if (CanAttack()){
            nextState = new Attack(npc,anim,player);
            stage = EVENT.EXIT;
        }
        //this probably makes no sense, need to rethink this once AI complete
        else if (controls.GetDistanceToTarget()<5.0f){
            nextState = new Avoid(npc,anim,player);
            stage = EVENT.EXIT;
        }
        
        
    }
    public override void Exit()
    {
        anim.ResetTrigger("isWalking");
        base.Exit();
    }
    
}
