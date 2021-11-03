using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : State
{
    public IdleState idleState;

    private bool oneTime;
    public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
    {
        //enemyAnimatorManager.anim.rootPosition = Vector3.zero;

        if(!oneTime)
        {
            enemyAnimatorManager.anim.enabled = false;
            enemyAnimatorManager.anim.enabled = true;


            enemyAnimatorManager.PlayTargetAnimation("Dying", true);

            enemyAnimatorManager.anim.SetBool("Die", true);
            oneTime = true;
        }
 
        return this;
    }
}
