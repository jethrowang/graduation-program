using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pass : MonoBehaviour
{
    static public bool level1Pass,level2Pass,level3Pass,level4Pass;
    public GameObject[] levelPassAnim;
    public float timeLeft,time;
    private PlayerInputActions controls;
    private void Awake()
    {
        controls = new PlayerInputActions();
        controls.Player.NoLevelPass.started += ctx => NoLevelPass();
    }
    void OnEnable()
    {
        controls.Player.Enable();
    }
    void OnDisable()
    {
        controls.Player.Disable();
    }
    void Update()
    {
        if(level1Pass)
        {
            levelPassAnim[0].SetActive(true);
        }else
        {
            levelPassAnim[0].SetActive(false);
        }
        if(level2Pass)
        {
            levelPassAnim[1].SetActive(true);
        }else
        {
            levelPassAnim[1].SetActive(false);
        }
        if(level3Pass)
        {
            levelPassAnim[2].SetActive(true);
        }else
        {
            levelPassAnim[2].SetActive(false);
        }
        if(level4Pass)
        {
            levelPassAnim[3].SetActive(true);
        }else
        {
            levelPassAnim[3].SetActive(false);
        }
        if(level1Pass && level2Pass && level3Pass && level4Pass)
        {
            Invoke("GotoEnding",2.3f);
        }
    }
    void GotoEnding()
    {
        NoLevelPass();
        GameObject.Find("levelLoader").GetComponent<levelLoader>().LoadSomeLevel(22);
    }
    void NoLevelPass()
    {
        level1Pass=false;
        level2Pass=false;
        level3Pass=false;
        level4Pass=false;
    }
}