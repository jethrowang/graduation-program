using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bgsound : MonoBehaviour
{
    public GameObject obje;
	GameObject obj=null;
    void Start()
    {
        obj = GameObject.FindGameObjectWithTag("sound");  
    	if (obj==null)
        {  
            obj = (GameObject)Instantiate(obje); 
        } 
    }
}
