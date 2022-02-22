using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovement : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    public PlayerState playerState;

    private Vector2 direction;
    [SerializeField] private float moveSpeed = 1f;

    public void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        //Detects input for player movement
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");

        //Disables diagonal movement
        if (direction.y != 0)
        {
            direction.x = 0;
        }
    }

    public void FixedUpdate()
    {
        if (playerState != PlayerState.dead)
        {
            //Moves the player
            rigidbody.velocity = direction * moveSpeed;
        }
    }
}
