using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tube : MonoBehaviour
{
    private Animator anim;
    public Camera cam;
    private Vector3 mousePos;
    private Vector2 arrowDirection;
    void Start()
    {
        anim=GetComponent<Animator>();
    }
    void Update()
    {
        mousePos=cam.ScreenToWorldPoint(Input.mousePosition);
        arrowDirection=(mousePos-transform.position).normalized;
        float angle=Mathf.Atan2(arrowDirection.y,arrowDirection.x)*Mathf.Rad2Deg;
        transform.eulerAngles=new Vector3(0,0,angle);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="trash")
        {
            Destroy(collision.gameObject);
            anim.Play("swallow");
            soundmanager.sound_instance.SwallowAudio();
            // GameObject.Find("ship").GetComponent<ship4>().CollectionsPlus();
        }
    }
}
