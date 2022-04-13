using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform : MonoBehaviour
{
    public float speed;
    public float waitTime;
    public Transform[] movePos;
    private int i;
    private Transform playerdeftransform;
    private Transform enemydeftransform;
    void Start()
    {
        i=1;
        playerdeftransform=GameObject.FindGameObjectWithTag("player").transform.parent;
        enemydeftransform=GameObject.FindGameObjectWithTag("enemy").transform.parent;
    }

    void Update()
    {
        transform.position=Vector2.MoveTowards(transform.position,movePos[i].position,speed*Time.deltaTime);
        if(Vector2.Distance(transform.position,movePos[i].position)<0.1f)
        {
            if(waitTime<0.0f)
            {
                if(i==0)
                {
                    i=1;
                }
                else
                {
                    i=0;
                }
                waitTime=2f;
            }
            else
            {
                waitTime-=Time.deltaTime;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("player")&&other.GetType().ToString()=="UnityEngine.BoxCollider2D")
        {
            // other.gameObject.transform.parent=gameObject.transform;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("player")&&other.GetType().ToString()=="UnityEngine.BoxCollider2D")
        {
            // other.gameObject.transform.parent=playerdeftransform;
        }
    }
}
