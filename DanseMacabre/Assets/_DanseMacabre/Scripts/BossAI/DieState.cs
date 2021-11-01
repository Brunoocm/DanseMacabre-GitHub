using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : State
{
    public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
    {
        enemyAnimatorManager.anim.SetBool("Die", true);
        return this;
    }
}
