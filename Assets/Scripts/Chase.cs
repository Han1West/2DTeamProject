using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : MonoBehaviour
{

    [SerializeField]
    [Range(1f, 1000f)] float moveSpeed = 2f;

    Animator animator;

    GroundFirst groundFirst;

    private protected Rigidbody2D rb;
    private protected Vector2 movement;
    public GameObject player;

    public bool canMove = true;

    private void Start()
    {
        groundFirst = FindObjectOfType<GroundFirst>();
        animator = GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
        player = GameObject.Find(tag = "Player");
    }
    private void Update()
    {
        Vector3 direction = player.transform.position - transform.position;
        if (transform.position.x > player.transform.position.x)
            animator.SetBool("Right", false);
        else
            animator.SetBool("Right", true);
        direction.Normalize();
        movement = direction;
       
    }
    private void FixedUpdate()
    {
        if(groundFirst.mon)
            MoveCharacter(movement);
    }
    private void MoveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    public void NotMove()
    {
        canMove = false;
    }

    public void CanMove()
    {
        canMove = true;
    }
}