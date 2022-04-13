using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow : MonoBehaviour
{
    
    public GameObject bullet;
    public Transform firepoint;
    public float firerate;
    public float nextfire;
    public Camera cam;
    private Vector3 mousePos;
    private Vector2 arrowDirection;
    void Update()
    {
        mousePos=cam.ScreenToWorldPoint(Input.mousePosition);
        arrowDirection=(mousePos-transform.position).normalized;
        float angle=Mathf.Atan2(arrowDirection.y,arrowDirection.x)*Mathf.Rad2Deg;
        transform.eulerAngles=new Vector3(0,0,angle);
        if(Input.GetMouseButtonDown(0))
        {
            if(Time.time>nextfire)
            {
                nextfire = Time.time + firerate;
                Instantiate(bullet,firepoint.position,Quaternion.Euler(transform.eulerAngles));
                soundmanager.sound_instance.Throwaudio();
            }
        }
    }
}
