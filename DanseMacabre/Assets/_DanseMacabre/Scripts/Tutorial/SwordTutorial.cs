using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordTutorial : MonoBehaviour
{

    MainTutorial scriptTutorial;
    void Start()
    {
        scriptTutorial = GameObject.Find("-----Tutorial-----").GetComponent<MainTutorial>();
    }

    void Update()
    {
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!scriptTutorial.hasSword)
            {
                GameObject.Find("PegarTutorial").transform.GetChild(0).gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    scriptTutorial.hasSword = true;
                    GameObject.Find("PegarTutorial").transform.GetChild(0).gameObject.SetActive(false);
                }
            }
          
        }
    }

    private void OnTriggerEnter(Collider other)
    {
    }
}
