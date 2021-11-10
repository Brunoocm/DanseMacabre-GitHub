using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnSystem : MonoBehaviour
{
    public bool isDead;

    public Vector3 m_spawn;
    private static RespawnSystem respawnObj;


    void Awake()
    {
        DontDestroyOnLoad(this);

        if (respawnObj == null)
        {
            respawnObj = this;
        }
        else
        {
            Destroy(gameObject);
        }

     
    }

    private void Start()
    {
        isDead = false;
    }
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    DieFunction(); //morre muito aqui
        //}
    }
    public void DieFunction()
    {
        SetSpawn();
        LoadSceneMorte();
    }
    public void Respawn()
    {
        FindObjectOfType<MainCheckpoint>().playerObj.transform.position = new Vector3(m_spawn.x, 
            FindObjectOfType<MainCheckpoint>().playerObj.transform.position.y, m_spawn.z);
    }
    void SetSpawn()
    {
        m_spawn = FindObjectOfType<MainCheckpoint>().spawn;
    }

    void LoadSceneMorte()
    {
        isDead = true;
        SceneManager.LoadSceneAsync("CenaMorte");
    }
    public void LoadSceneInicial()
    {
        SceneManager.LoadSceneAsync("cenario");
    }
}
