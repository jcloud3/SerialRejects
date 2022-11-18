using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : State
{
    public Attack(GameObject _npc, Animator _anim, Transform _player):base(_npc,_anim,_player)
    {
        name = STATE.ATTACK;
        //controls = npc.GetComponent<EnemyMove>();
    }
    public override void Enter()
    {
        anim.SetTrigger("isAttacking");
        base.Enter();
    }
    public override void Update()
    {
        if(!CanAttack()){
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
