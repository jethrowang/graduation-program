using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cake : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        rb.velocity=transform.right*-speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="player")
        {
            Destroy(gameObject,0.1f);
        }
        if(collision.gameObject.tag=="platform")
        {
            soundmanager.sound_instance.Cakeaudio();
            Destroy(gameObject,0.3f);
        }
        if(collision.gameObject.tag=="bullet")
        {
            soundmanager.sound_instance.Cakeaudio();
            Destroy(gameObject,0.1f);
        }
    }
}
