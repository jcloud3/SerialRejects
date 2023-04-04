using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avoid : State
{
    //can use this to track which player is being pursued
   int currentPlayer = -1;
   float maxAvoidDistance = 8.5f;
   float randomPursueFloat = .005f;
   float minAvoidDistance = 3.5f;
   float randomAttack = .3f;
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
        if (CanAttack() && Random.Range(0f,1f)<randomAttack){
            nextState = new Attack(npc,anim,player);
            stage = EVENT.EXIT;
        }
        else if (controls.GetDistanceToTarget() > minAvoidDistance){

            if (Random.Range(0f,1f)<randomPursueFloat){
                    nextState = new Pursue(npc,anim,player);
                    stage = EVENT.EXIT;
                }
            
            else if (controls.GetDistanceToTarget()>maxAvoidDistance){
                nextState = new Idle(npc,anim,player);
                stage = EVENT.EXIT;
            }
        }
        
        
    }
    public override void Exit()
    {
        anim.ResetTrigger("isWalking");
        base.Exit();
    }
    
}
