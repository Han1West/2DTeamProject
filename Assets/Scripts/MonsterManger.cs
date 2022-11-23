using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManger : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;

    public int moveDir;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("monsterAI"); 
    }

    // Update is called once per frame
    void Update()
    {
        if (rigid.velocity.x > 0.1f)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    void FixedUpdate()
    {
        rigid.velocity = new Vector2(moveDir, rigid.velocity.y);
    }

    IEnumerator monsterAI()
    {
        moveDir = Random.Range(-1, 2);
        yield return new WaitForSeconds(5f);

        StartCoroutine("monsterAI");
    }

    public void startMove()
    {
        StartCoroutine("monsterAI");
    }

    public void stopMove()
    {
        StartCoroutine("monsterAI");
    }
}
