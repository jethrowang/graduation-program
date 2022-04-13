using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pass4 : MonoBehaviour
{
    void Update()
    {
        if(GameObject.Find("ship").GetComponent<ship4>().Pass())
        {
            pass.level4Pass=true;
        }
    }
}
