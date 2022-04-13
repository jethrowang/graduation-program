using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pass1 : MonoBehaviour
{
    void Update()
    {
        if(GameObject.Find("player").GetComponent<player>().Pass())
        {
            pass.level1Pass=true;
        }
    }
}
