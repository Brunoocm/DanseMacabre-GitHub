using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public PersueTargetState pursueTargetState;

    public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
    {
        HealthSystem characterState = FindObjectOfType<HealthSystem>();
        enemyManager.currentTarget = characterState;

        if(enemyManager.currentTarget != null)
        {
            return pursueTargetState;
        }
        else
        {
            return this;

        }
    }
}
