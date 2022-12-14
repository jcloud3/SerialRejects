using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : State
{
    float randomAttackChance = .5f;
    float minAvoidDistance = 2.5f;
    public Idle(GameObject _npc, Animator _anim, Transform _player):base(_npc,_anim,_player)
    {
        name = STATE.IDLE;
        //controls = npc.GetComponent<EnemyMove>();
    }
    public override void Enter()
    {
        Debug.Log("Idle");
        anim.SetTrigger("isIdle");
        base.Enter();
    }
    public override void Update()
    {
        if (CanAttack()&& Random.Range(0f,1f)<randomAttackChance){
            nextState = new Attack(npc,anim,player);
            stage = EVENT.EXIT;
        }
        if(controls.FindTarget()!=null){
            if(controls.GetDistanceToTarget()<minAvoidDistance){
                nextState = new Avoid(npc,anim,player);
                stage = EVENT.EXIT;
            }
            else{
                nextState = new Pursue(npc,anim,player);
                stage = EVENT.EXIT;
            }
            
        }
        
    }
    public override void Exit()
    {
        anim.ResetTrigger("isIdle");
        base.Exit();
    }
}
