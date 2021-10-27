using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Cutscene : MonoBehaviour
{
    public VideoClip videoItem;

    VideoScene UIvid;
    Camera mainCam;


    void Start()
    {
        mainCam = Camera.main;
        UIvid = FindObjectOfType<VideoScene>();
        //if (videoItem != null) Video Player;
        PlayVideo();

    }

    private void Update()
    {
      

    }



    void PlayVideo()
    {
        UIvid.vidObject.SetActive(true);
        UIvid.SetVideo(videoItem);
        Time.timeScale = 0;

    }

  

}
