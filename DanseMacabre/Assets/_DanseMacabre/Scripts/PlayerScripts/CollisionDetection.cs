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

            print("hit");  
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy")) //precisava colocar a tag
        {
            enemy = null;
        }
       
    }

    public void TryDamage()
    {
        //coloquei o collider na espada pra rotacionar junto com o jogador e ficar mais preciso
        if (statesSystem.combatState == States.CombatState.attacking && enemy != null)
        {
            enemy.GetComponent<InimigoPrototipo>().PlayAnimation(statesSystem.attackType);
        }
    }
}
