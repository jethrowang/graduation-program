using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class salivacreator : MonoBehaviour
{
    public float speed;
    public bool isgoingright;
    public bool createsaliva;
    public float timer;
    public float cd_time;
    public GameObject[] saliva_array;
    public GameObject player;
    public float distance;
    void FixedUpdate()
    {
        Movement();
    }

    void Update()
    {
        Create();
    }

    void Movement()
    {
        //超過右邊界
        if (transform.position.x >= player.transform.position.x + distance)
        {
            isgoingright = false;
        }
        //超過左邊界
        if (transform.position.x <= player.transform.position.x - distance)
        {
            isgoingright = true;
        }
        //布林控制移動
        if (isgoingright == true)
        {
            transform.Translate(Vector2.right * Time.deltaTime * speed);
        }
        else
        {
            transform.Translate(Vector2.left * Time.deltaTime * speed);
        }
    }

    void Create()
    {
        if (createsaliva == true)
        {
            Instantiate(saliva_array[Random.Range(0,saliva_array.Length)], transform.position, Quaternion.identity);
            createsaliva = false;
        }
        //計時器timer
        //時間到→生成一個地板→重新計時→
        timer += Time.deltaTime;
        if (timer >= cd_time)
        {
            createsaliva = true;
            timer = 0;
        }
    }
}
