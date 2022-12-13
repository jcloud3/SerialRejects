using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : State
{
    public Idle(GameObject _npc, Animator _anim, Transform _player):base(_npc,_anim,_player)
    {
        name = STATE.IDLE;
        //controls = npc.GetComponent<EnemyMove>();
    }
    public override void Enter()
    {
        Debug.Log("Start Idle");
        anim.SetTrigger("isIdle");
        base.Enter();
    }
    public override void Update()
    {
        if (CanAttack()){
            nextState = new Attack(npc,anim,player);
            stage = EVENT.EXIT;
        }
        if(controls.FindTarget()!=null){
            nextState = new Pursue(npc,anim,player);
            stage = EVENT.EXIT;
        }
        
    }
    public override void Exit()
    {
        anim.ResetTrigger("isIdle");
        base.Exit();
    }
}
