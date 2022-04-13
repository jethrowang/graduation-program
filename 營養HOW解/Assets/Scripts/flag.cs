using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flag : MonoBehaviour
{
    public GameObject newFlag;
    public GameObject hitParticles;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="ball")
        {
            Destroy(collision.gameObject);
            Instantiate(hitParticles,transform.position,Quaternion.identity);
            GameObject.Find("ship3-2").GetComponent<ship>().CollectionsPlus();
            soundmanager.sound_instance.FlagAudio();
            newFlag.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
