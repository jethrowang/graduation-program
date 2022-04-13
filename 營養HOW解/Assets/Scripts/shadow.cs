using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shadow : MonoBehaviour
{
    private Transform player;
    private SpriteRenderer thisSprite;
    private SpriteRenderer playerSprite;
    private Color color;
    public float activeTime;//顯示時間
    public float activeStart;//開始顯示的時間點
    private float alpha;
    public float alphaSet;//初始值
    public float alphaMultiplier;
    private void OnEnable()
    {
        player=GameObject.FindGameObjectWithTag("player").transform;
        thisSprite=GetComponent<SpriteRenderer>();
        playerSprite=player.GetComponent<SpriteRenderer>();
        alpha=alphaSet;
        thisSprite.sprite=playerSprite.sprite;
        transform.position=player.position;
        transform.localScale=player.localScale;
        transform.rotation=player.rotation;
        activeStart=Time.time;
    }
    void Update()
    {
        alpha*=alphaMultiplier;
        color=new Color(1,1,1,alpha);
        thisSprite.color=color;
        if(Time.time>=activeStart+activeTime)
        {
            shadowPool.instance.ReturnPool(this.gameObject);//返回對象池
        }
    }
}
