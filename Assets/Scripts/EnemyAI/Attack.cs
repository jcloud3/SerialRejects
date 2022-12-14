using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : State
{
    private bool isInAttackAnimation = false;
    float randomAvoidFloat = .1f;
    public Attack(GameObject _npc, Animator _anim, Transform _player):base(_npc,_anim,_player)
    {
        name = STATE.ATTACK;
        //controls = npc.GetComponent<EnemyMove>();
    }
    public override void Enter()
    {
        
        anim.SetTrigger("isAttacking");
        isInAttackAnimation = true;
        base.Enter();
    }
    public override void Update()
    {
        if(anim.GetAnimatorTransitionInfo(0).IsName("Attack_A -> Idle")){
            isInAttackAnimation = false;
        }
        //controls.Move();
        if(!CanAttack()){
            nextState = new Pursue(npc, anim,player);
            stage = EVENT.EXIT;
        }
        else if (Random.Range(0f,1f)<randomAvoidFloat){
                nextState = new Avoid(npc,anim,player);
                stage = EVENT.EXIT;
            }

        else if(!isInAttackAnimation){
            nextState = new Idle(npc, anim,player);
            
            stage = EVENT.EXIT;
        }

        
    }
    public override void Exit()
    {
        anim.ResetTrigger("isAttacking");
        base.Exit();
    }
}
