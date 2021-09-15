using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordTutorial : MonoBehaviour
{

    MainTutorial scriptTutorial;
    GameObject atacarObj;
    void Start()
    {
        scriptTutorial = GameObject.Find("-----Tutorial-----").GetComponent<MainTutorial>();
        atacarObj = GameObject.Find("PegarTutorial");
    }

    void Update()
    {
      
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (scriptTutorial.hasSword)
            {
                atacarObj.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    scriptTutorial.hasSword = true;
                    atacarObj.SetActive(false);
                    print("aaa");
                }
            }
          
        }
    }
}
