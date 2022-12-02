using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieContro : MonoBehaviour
{

    Rigidbody2D rb;
    Transform target;

    [Header("추격 속도")]
    [SerializeField]
    [Range(1f, 4f)] float moveSpeed = 3f;

    [Header("근접 거리")]
    [SerializeField]
    [Range(0f, 3f)] float contactDistance = 1f;

    bool follow = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("monsterAI"); 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        follow = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        follow = false;
    }
    // Update is called once per frame
    void Update()
    {
        FollowTarget();
    }

    void FollowTarget()
    {
        if (Vector2.Distance(transform.position, target.position) > contactDistance && follow)
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        else
            rb.velocity = Vector2.zero;
    }
}
