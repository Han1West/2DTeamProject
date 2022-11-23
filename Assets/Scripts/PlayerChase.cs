using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChase : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            transform.parent.GetComponent<MonsterManger>().stopMove();
            Vector3 playersPos = collision.transform.position;
            if(playersPos.x > transform.position.x)
            {
                transform.parent.GetComponent<MonsterManger>().moveDir = 3;
            }
            else if(playersPos.x < transform.position.x)
            {
                transform.parent.GetComponent<MonsterManger>().moveDir = -3;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
            transform.parent.GetComponent<MonsterManger>().startMove();
    }
}
