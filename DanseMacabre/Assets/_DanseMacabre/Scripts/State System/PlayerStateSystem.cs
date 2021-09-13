using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateSystem : StatesSystem
{
    public MovementHandler movementHandler => GetComponent<MovementHandler>();
    private CombatSystem combatSystem => GetComponent<CombatSystem>();

    private void Update()
    {
        UpdateCurrentInfo( );
        UpdateMovementHandler( );
        UpdateCombatSystem( );
    }
    public void UpdateCurrentInfo()
    {
        currentEnemy = combatSystem.currentEnemy;
        hasEnemy = combatSystem.hasEnemy;
    }

    public void UpdateMovementHandler()
    {

    }

    public void UpdateCombatSystem()
    {

    }


}
