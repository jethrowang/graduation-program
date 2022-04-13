using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    public GameObject hitParticles;
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        rb.velocity=transform.right*speed;
        Destroy(gameObject,0.5f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="enemy")
        {
            Destroy(gameObject);
            Instantiate(hitParticles,transform.position,Quaternion.identity);
        }
        if(collision.gameObject.tag=="boss")
        {
            Destroy(gameObject);
            Instantiate(hitParticles,transform.position,Quaternion.identity);
        }
        if(collision.gameObject.tag=="platform")
        {
            Destroy(gameObject);
            Instantiate(hitParticles,transform.position,Quaternion.identity);
        }
        if(collision.gameObject.tag=="cake")
        {
            Destroy(gameObject);
            Instantiate(hitParticles,transform.position,Quaternion.identity);
        }
        if(collision.gameObject.tag=="collection")
        {
            Destroy(gameObject);
            Instantiate(hitParticles,transform.position,Quaternion.identity);
        }
        if(collision.gameObject.tag=="flag")
        {
            Destroy(gameObject);
            Instantiate(hitParticles,transform.position,Quaternion.identity);

        }
    }
}
