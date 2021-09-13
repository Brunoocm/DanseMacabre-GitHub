using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class States : MonoBehaviour
{
    [Header("States")]
    public MasterState masterState = MasterState.idle;
    public CombatState combatState = CombatState.inactive;

    public enum MasterState
    {
        idle,
        walking,
        combat,
        dead,
    }

    public enum CombatState
    {
        inactive,
        idle,
        attacking,
        defending,
        stunned,
    }
}
