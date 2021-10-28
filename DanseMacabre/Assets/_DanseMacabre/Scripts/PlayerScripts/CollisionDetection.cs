using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public int damage;
    private PlayerStateSystem statesSystem => GetComponent<PlayerStateSystem>();
    private CombatSystem combat => GetComponent<CombatSystem>();
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

            if (combat.attackType == 1)
                enemy.GetComponent<InimigoPrototipo>().Damage(damage);
            else if (combat.attackType == 2)
                enemy.GetComponent<InimigoPrototipo>().Damage(damage * 2);


        }
    }
}
