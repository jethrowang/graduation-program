using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pass3 : MonoBehaviour
{
    void Update()
    {
        if(GameObject.Find("ship3-2").GetComponent<ship>().Pass())
        {
            pass.level3Pass=true;
        }
    }
}
