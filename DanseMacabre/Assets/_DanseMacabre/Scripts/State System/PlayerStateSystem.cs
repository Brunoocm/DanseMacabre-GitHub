using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateSystem : StatesSystem
{
    public MovementHandler movementHandler => GetComponent<MovementHandler>( );
    private CombatSystem combatSystem => GetComponent<CombatSystem>( );
    private AnimationHandler animationHandler => GetComponent<AnimationHandler>( );

    private void Update()
    {
        inCombat = combatSystem.hasEnemy;

        UpdateCurrentInfo( );
        UpdateMovementHandler( );
        UpdateCombatSystem( );
        UpdateAnimationHandler( );
    }
    public void UpdateCurrentInfo()
    {
        currentEnemy = combatSystem.currentEnemy;
        inCombat = combatSystem.hasEnemy;
    }

    public void UpdateMovementHandler()
    {
        movementHandler.RotateTowardTarget( currentEnemy.enemy , inCombat );
    }

    public void UpdateCombatSystem()
    {

    }

    public void UpdateAnimationHandler()
    {
        animationHandler.InCombat( inCombat );
    }
}
