using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class mouse : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IEventSystemHandler
{
    private Vector3 intialPos;
    void Start()
    {
        intialPos=gameObject.transform.position;
    }
    //進入
    public void OnPointerEnter(PointerEventData eventData)
    {
        soundmanager.sound_instance.Hoveraudio();
        transform.position = transform.position + new Vector3(0, 5f, 0);
    }

    //離開
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.position = intialPos;
    }

    //按下
    public void OnPointerDown(PointerEventData eventData)
    {
        soundmanager.sound_instance.Pressaudio();
        transform.position = transform.position + new Vector3(0, -10f, 0);
    }

    //彈起
    public void OnPointerUp(PointerEventData eventData)
    {
        transform.position = intialPos;
    }
}