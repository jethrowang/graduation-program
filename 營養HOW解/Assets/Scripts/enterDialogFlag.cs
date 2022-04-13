using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enterDialogFlag : MonoBehaviour
{
    public GameObject dialog;
    public GameObject hitParticles;
    public GameObject black;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="ball")
        {
            Destroy(collision.gameObject);
            Instantiate(hitParticles,transform.position,Quaternion.identity);
            Time.timeScale=0f;
            dialog.SetActive(true);
            black.SetActive(true);
        }
    }
}
