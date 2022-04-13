using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgmController1 : MonoBehaviour
{
    private void Awake()
    {
        Destroy(GameObject.FindGameObjectWithTag("sound"));
    }
}
