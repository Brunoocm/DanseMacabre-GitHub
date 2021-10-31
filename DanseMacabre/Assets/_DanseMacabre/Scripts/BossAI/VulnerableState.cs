using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VulnerableState : State
{
    public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
    {
        enemyAnimatorManager.anim.SetBool("Vulnerable",true);

        return this;
    }
}
