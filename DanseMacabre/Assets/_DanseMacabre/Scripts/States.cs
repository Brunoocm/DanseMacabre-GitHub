using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class States 
{
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
