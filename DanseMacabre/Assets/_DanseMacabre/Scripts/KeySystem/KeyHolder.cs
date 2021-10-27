using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class KeyHolder : MonoBehaviour
{
    public Collider coll;

    [Header("Text")]
    public GameObject display;
    public TextMeshProUGUI textPro;

    [Header("Door")]
    public string messageLocked;
    public string messageOpened;

    [Header("Key")]
    public string messagePickUp;

    public List<string> nameObj;
    private bool canOpen;
    void Start()
    {
        nameObj = new List<string>();
   
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("DoorLocked"))
        {
            display.SetActive(true);
            textPro.text = messageOpened;
       
        }
        else if (other.gameObject.CompareTag("Key"))
        {
            display.SetActive(true);
            textPro.text = messagePickUp;
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("DoorLocked"))
        {
            DoorLocked door = other.GetComponent<DoorLocked>();

            if (nameObj.Contains(door.doorName))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    door.OpenTheDoor();
                    display.SetActive(false);
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    textPro.text = messageLocked;
                }
            }
        }
        else if (other.gameObject.CompareTag("Key"))
        {
            KeyObject key = other.GetComponent<KeyObject>();

            if(Input.GetKeyDown(KeyCode.E))
            {
                if (!nameObj.Contains(key.keyName))
                {
                    nameObj.Add(key.keyName);
                    Destroy(other.gameObject);
                    display.SetActive(false);
                }
                else
                {
                    print("já tem");
                }
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("DoorLocked"))
        {
            display.SetActive(false);
        }
        if (other.gameObject.CompareTag("Key"))
        {
            display.SetActive(false);
        }
    }
}
