using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Video;

public class VideoScene : MonoBehaviour
{
    public GameObject vidObject;
    public VideoPlayer vid;

    void Update()
    {
        if ((vid.frame) > 0 && (vid.isPlaying == false)) ExitVideo();
    }

    public void SetVideo(VideoClip vidRender)
    {
        vid.clip = vidRender;
    }

    void ExitVideo()
    {
        vidObject.SetActive(false);
        Time.timeScale = 1;
    }
}
