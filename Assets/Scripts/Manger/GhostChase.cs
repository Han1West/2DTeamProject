using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostChase : MonoBehaviour
{

    [SerializeField]
    [Range(1f, 1000f)] float moveSpeed = 2f;

    [SerializeField]
    [Range(1f, 1000f)] float moveSpeed2 = 2f;

    Animator animator;

    GroundFirst groundFirst;

    private protected Rigidbody2D rb;
    private protected Vector2 movement;
    private PlayerManager thePlayer;
    
    public GameObject player;

    public bool canMove = true;
    public bool rightVector;

    private void Start()
    {
        thePlayer = FindObjectOfType<PlayerManager>();
        groundFirst = FindObjectOfType<GroundFirst>();
        animator = GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
        player = GameObject.Find(tag = "Player");
    }
    private void Update()
    {
        Vector3 direction = player.transform.position - transform.position;
        if (transform.position.x > player.transform.position.x)
        {
            animator.SetBool("Right", false);
            rightVector = false;
        }
        else
        {
            animator.SetBool("Right", true);
            rightVector = true;
        }

        direction.Normalize();
        movement = direction;

    }
    private void FixedUpdate()
    {
        if (rightVector = thePlayer.rightTargetVector)
            MoveCharacter(movement);
        else
            BackCharacter(movement);
    }
    private void MoveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    private void BackCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position - (direction * moveSpeed2 * Time.deltaTime));
    }
}