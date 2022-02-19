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
    hit,
    dead
}

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private LookDirection lookDirection;
    [SerializeField] private PlayerState playerState;
    [SerializeField] private Animator animator;

    [SerializeField] private float xAxis;
    [SerializeField] private float yAxis;
    [SerializeField] private bool fire1;
    [SerializeField] private bool fire2;

    private void Start()
    {
        animator.GetComponent<Animator>();
    }

    private void Update()
    {   
        ////Changes the animation
        //if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        //{
        //    lookDirection = LookDirection.down;
        //    playerState = PlayerState.walking;
        //}
        //else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        //{
        //    lookDirection = LookDirection.left;
        //    playerState = PlayerState.walking;
        //}
        //else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        //{
        //    lookDirection = LookDirection.right;
        //    playerState = PlayerState.walking;
        //}
        //else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        //{
        //    lookDirection = LookDirection.up;
        //    playerState = PlayerState.walking;
        //}
        //else
        //{
        //    playerState = PlayerState.idle;
        //}

        //animator.SetFloat("LookDirection", (float)lookDirection);
        //animator.SetInteger("PlayerState", (int)playerState);

        CheckMovement();
    }

    private void CheckMovement()
    {
        xAxis = Input.GetAxis("Horizontal");
        yAxis = Input.GetAxis("Vertical");

        fire1 = Input.GetButtonDown("Fire1"); //z
        fire2 = Input.GetButtonDown("Fire2"); //x

        if (xAxis == 0 & yAxis == 0)
        {
            playerState = PlayerState.idle;
        }
        else
        {
            playerState = PlayerState.walking;
        }

        if (yAxis > 0)
        {
            xAxis = 0;
            lookDirection = LookDirection.up;
        }
        else if (yAxis < 0)
        {
            xAxis = 0;
            lookDirection = LookDirection.down;
        }
        else if (xAxis > 0)
        {
            yAxis = 0;
            lookDirection = LookDirection.right;
        }
        else if (xAxis < 0)
        {
            yAxis = 0;
            lookDirection = LookDirection.left;
        }

        if (fire1)
        {
            print("z pressed");
        }
        if (fire2)
        {
            playerState = PlayerState.attacking;
        }

        animator.SetInteger("PlayerState", (int)playerState);
        animator.SetFloat("LookDirection", (float)lookDirection);
    }

    public void SetAnimPlayerState(PlayerState stateInt)
    {
        animator.SetInteger("PlayerState", (int)playerState);
    }

    public LookDirection GetDirection()
    {
        return lookDirection;
    }
}
