using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private LookDirection lookDirection;
    [SerializeField] private PlayerState playerState;
    [SerializeField] private Animator animator;

    [Header("Input")]
    [SerializeField] private float xAxis;
    [SerializeField] private float yAxis;
    [SerializeField] private bool fire1;
    [SerializeField] private bool fire2;
    [SerializeField] private Color[] flashColors;

    private SpriteRenderer playerRenderer;


    private void Start()
    {
        animator.GetComponent<Animator>();
        playerState = PlayerState.idle;
        lookDirection = LookDirection.down;
        playerRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {   
        CheckMovement();
        UseItemSlot();
    }

    //Checks for movement input
    private void CheckMovement()
    {
        //If player isn't dead, allows animations
        if (playerState != PlayerState.dead)
        {
            xAxis = Input.GetAxis("Horizontal");
            yAxis = Input.GetAxis("Vertical");

            //If player doesnt move enable idle animation
            if (xAxis == 0 & yAxis == 0)
            {
                playerState = PlayerState.idle;
            }
            //Else enables walking animation
            else
            {
                playerState = PlayerState.walking;
            }
            animator.SetInteger("PlayerState", (int)playerState);

            //Changes the direction of player
            if (yAxis > 0)
            {
                lookDirection = LookDirection.up;
            }
            else if (yAxis < 0)
            {
                lookDirection = LookDirection.down;
            }
            else if (xAxis > 0)
            {
                lookDirection = LookDirection.right;
            }
            else if (xAxis < 0)
            {
                lookDirection = LookDirection.left;
            }
            animator.SetFloat("LookDirection", (float)lookDirection);
        }
    }

    private void UseItemSlot()
    {
        //If MB2 is pressed, the player attacks
        if (Input.GetButtonDown("Fire2"))
        {
            playerState = PlayerState.attacking;
            animator.SetInteger("PlayerState", (int)playerState);
        }
    }

    //If player is hit enable hit animation
    public void Hit()
    {
        StartCoroutine(FlashSprite(0.1f));
    }

    //Coroutine for flashing colors that appear when hit
    IEnumerator FlashSprite(float flashTime)
    {
        for (int i = 0; i < flashColors.Length; i++)
        {
            playerRenderer.material.color = flashColors[i];
            yield return new WaitForSeconds(flashTime);
        }
    }

    //Shows death animation
    public void TriggerDeathAnimation()
    {
        gameObject.GetComponent<PlayerMovement>().playerState = PlayerState.dead;
        playerState = PlayerState.dead;
        animator.SetTrigger("Death");
        animator.SetInteger("PlayerState", (int)playerState);
    }

    //Returns lookdirection
    public LookDirection GetDirection()
    {
        return lookDirection;
    }
}
