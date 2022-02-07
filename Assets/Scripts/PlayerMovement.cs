using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovement : MonoBehaviour
{
    private new Rigidbody2D rigidbody;

    private Vector2 direction;
    [SerializeField] private float moveSpeed = 1f;

    public void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        //static float GetAxisRaw(string Horizontal);
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");

        if (direction.y != 0)
        {
            direction.x = 0;
        }
    }

    public void FixedUpdate()
    {
       rigidbody.velocity = direction * moveSpeed;
    }
}
