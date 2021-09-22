using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    private PlayerStateSystem statesSystem => GetComponent<PlayerStateSystem>();

    public Collider enemy;

    private void Awake() => enemy = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemy = other;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        enemy = null;
    }

    public void TryDamage()
    {
        if (statesSystem.combatState == States.CombatState.attacking && enemy != null)
        {
            enemy.GetComponent<InimigoPrototipo>().PlayAnimation(statesSystem.attackType);
        }
    }
}
