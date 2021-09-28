using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float health;
    public float stamina;

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }
    public void LoadPlayer()
    {
        PlayerSave data = SaveSystem.LoadPlayer();

        health = data.heath;
        stamina = data.stamina;

        Vector3 pos;
        pos.x = data.playerPos[0];
        pos.y = data.playerPos[1];
        pos.z = data.playerPos[2];
        transform.position = pos;
    }
}
