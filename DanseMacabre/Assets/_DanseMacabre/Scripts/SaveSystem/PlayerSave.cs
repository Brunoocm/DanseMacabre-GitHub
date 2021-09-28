using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerSave
{
    public float heath;
    public float stamina;
    public float[] playerPos;
    public bool[] checkpoint;

    public PlayerSave (HealthSystem player)
    {
        heath = player.health;
        stamina = player.stamina;

        playerPos = new float[3];
        playerPos[0] = player.transform.position.x;
        playerPos[1] = player.transform.position.y;
        playerPos[2] = player.transform.position.z;

        //salvar resolução
    }
}
