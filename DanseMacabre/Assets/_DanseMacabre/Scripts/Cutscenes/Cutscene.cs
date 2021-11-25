using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Cutscene : MonoBehaviour
{
    public VideoClip[] cutscenes;
    public GameObject fim;
    [HideInInspector] public int currentCutscene;
    VideoScene UIvid;
    Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
        UIvid = FindObjectOfType<VideoScene>();
        currentCutscene = 0;
        //if (videoItem != null) Video Player;
    }

    public void PlayVideo()
    {
        UIvid.vidObject.SetActive(true);
        UIvid.SetVideo(cutscenes[currentCutscene]);
        Time.timeScale = 0;
        currentCutscene++;
        if (currentCutscene > cutscenes.Length) Fim();
    }

    public void Fim()
    {
        fim.SetActive(true);
    }
}
