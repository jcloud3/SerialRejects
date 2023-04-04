using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    public enum STATE{
        IDLE, PURSUE, ATTACK, AVOID
    };
    public enum EVENT{
        ENTER, UPDATE, EXIT
    };
    public STATE name;
    protected EVENT stage;
    protected GameObject npc;
    protected Animator anim;
    protected Transform player;
    protected State nextState;


    public EnemyMove controls;
    float attackDist = 6.0f;
    float attackAngle = 12.0f;

    public State(GameObject _npc, Animator _anim, Transform _player){
        npc = _npc;
        anim = _anim;
        stage = EVENT.ENTER;
        player = _player;
        controls = npc.GetComponent<EnemyMove>();
    }

    public virtual void Enter(){stage = EVENT.UPDATE;}
    public virtual void Update(){stage = EVENT.UPDATE;}
    public virtual void Exit(){ stage = EVENT.EXIT;}

    public State Process(){
        if(stage == EVENT.ENTER) Enter();
        if(stage == EVENT.UPDATE) Update();
        if(stage == EVENT.EXIT) 
        {
            Exit();
            return nextState;
        }
        return this;
    }
    public bool CanAttack(){
        if(controls.GetDistanceToTarget() < attackDist){
            
            Vector2 direction = player.position-npc.transform.position;
            float angle = Vector2.Angle(direction,-npc.transform.right);
            
            if(angle>90){
                controls.flip();
            }
            if(angle<attackAngle){
                
                
                return true;
            }
            
            
            
        }
        return false;
    }
}
