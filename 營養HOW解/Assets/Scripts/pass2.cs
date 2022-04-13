using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pass2 : MonoBehaviour
{
    void Update()
    {
        if(GameObject.Find("player2").GetComponent<player>().Pass())
        {
            pass.level2Pass=true;
        }
    }
}
