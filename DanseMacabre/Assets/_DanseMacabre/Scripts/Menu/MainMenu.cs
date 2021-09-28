using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;


public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject loadScreen;
    [SerializeField] private Slider slider;

    public UnityEvent eventClick;

    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            eventClick.Invoke();
        }
    }
    public void ChangeScene(string name)
    {
        StartCoroutine(LoadAsync(name));

    }

    IEnumerator LoadAsync(string name)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(name);

        loadScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;

            yield return null;
        }
    }

    public void Quality(int currentQuality)
    {
        QualitySettings.SetQualityLevel(currentQuality);
    }
}
