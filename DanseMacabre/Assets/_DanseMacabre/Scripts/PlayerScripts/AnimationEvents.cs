using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    private CombatSystem combatSystem => GetComponentInParent<CombatSystem>();
    private HealthSystem healthSystem => GetComponentInParent<HealthSystem>();

    public void EndAttack(int type)
    {
        if (type == 1)
        {
            if (combatSystem.attackType == 2) return;
            else if (combatSystem.attackType == 1) combatSystem.attacking = false;
        }
        else
        {
            combatSystem.attacking = false;
        }
    }

    public void TryDamage()
    {
        GetComponentInParent<CollisionDetection>().TryDamage();
    }  
    public void AttackVoid()
    {
        GetComponentInParent<CollisionDetection>().AttackVoid();
    }  
    public void EndAttackVoid()
    {
        GetComponentInParent<CollisionDetection>().EndAttackVoid();
    }

    public void EnableMove()
    {
        healthSystem.EnableMove();
    }  
    public void DesableMove()
    {
        healthSystem.DesableMove();
    }
}
