using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatHandler : MonoBehaviour
{
    public GameObject cheat;
    private bool cheatando;

    void Start()
    {
        cheatando = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X)) cheatando = !cheatando;


        if (!cheatando)
        {
            GetComponent<CollisionDetection>().damage = 2;
            GetComponent<HealthSystem>().imortal = false;
            cheat.SetActive(false);
        }
        else
        {
            GetComponent<CollisionDetection>().damage = 20;
            GetComponent<HealthSystem>().imortal = true;
            cheat.SetActive(true);
        }
    }
}
