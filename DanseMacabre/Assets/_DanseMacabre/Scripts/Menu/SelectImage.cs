using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class SelectImage : MonoBehaviour, ISelectHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public Sprite image;
    GameObject filhoObj;

    public UnityEvent eventClick;
    void Start()
    {
        gameObject.GetComponentInChildren<Image>().sprite = image;
        filhoObj = this.gameObject.transform.GetChild(0).gameObject;
        filhoObj.SetActive(false);

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        print("enter");
        filhoObj.SetActive(true);
 
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        print("exit");
        filhoObj.SetActive(false);

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        eventClick.Invoke();
    }

    public void OnSelect(BaseEventData eventData)
    {
        eventData.selectedObject = gameObject;
        
    }


}
