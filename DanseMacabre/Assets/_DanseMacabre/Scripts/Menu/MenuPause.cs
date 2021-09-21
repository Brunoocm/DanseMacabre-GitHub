using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MenuPause : MonoBehaviour
{
    public GameObject pauseObj;
    public bool paused;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Resume();
        }
    }

    public void Resume()
    {
        paused = paused ? false : true;

        if (paused)
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            pauseObj.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            pauseObj.SetActive(false);
        }
    }

    public void Quality(int currentQuality)
    {
        QualitySettings.SetQualityLevel(currentQuality);
    }
}
