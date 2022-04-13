using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saliva : MonoBehaviour
{
    public GameObject saliva_particles;
    private Collider2D coll;
    
    void Start()
    {
        coll=GetComponent<Collider2D>();
        Destroy(gameObject, 7f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            soundmanager.sound_instance.Attackaudio();
            Destroy(this.gameObject);
            if(!player.isInvincible)
            {
                Instantiate(saliva_particles,transform.position,Quaternion.identity);
            }
        }
        if(collision.gameObject.CompareTag("enemy")||collision.gameObject.CompareTag("platform")||collision.gameObject.CompareTag("collection")||collision.gameObject.CompareTag("boss"))
        {
            soundmanager.sound_instance.Flooraudio();
            Destroy(this.gameObject);
            Instantiate(saliva_particles,transform.position,Quaternion.identity);
        }
    }
}
