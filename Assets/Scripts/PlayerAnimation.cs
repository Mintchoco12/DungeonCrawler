using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LookDirection
{
    down = 0,
    left,
    right,
    up
}

public enum PlayerState
{
    idle = 0,
    walking,
    attacking,
    hit
}

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private LookDirection lookDirection;
    [SerializeField] private PlayerState playerState;
    [SerializeField] private Animator animator;

    private void Start()
    {
        animator.GetComponent<Animator>();
    }

    private void Update()
    {   
        //Changes the animation
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            lookDirection = LookDirection.down;
            playerState = PlayerState.walking;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            lookDirection = LookDirection.left;
            playerState = PlayerState.walking;
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            lookDirection = LookDirection.right;
            playerState = PlayerState.walking;
        }
        else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            lookDirection = LookDirection.up;
            playerState = PlayerState.walking;
        }
        else
        {
            playerState = PlayerState.idle;
        }

        animator.SetFloat("LookDirection", (float)lookDirection);
        animator.SetInteger("PlayerState", (int)playerState);
    }
}
