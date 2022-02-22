using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private GameObject[] sword = new GameObject[4];
    [SerializeField] private Vector3[] boxOffset = new Vector3[4];
    [SerializeField] private Vector3[] boxSize = new Vector3[4];
    [SerializeField] private LayerMask layerToHit;

    [SerializeField] private Vector3 boxOffset1;
    [SerializeField] private Vector3 boxSize1;

    private LookDirection lookDirecton;
    private PlayerState playerState;
    private PlayerAnimation playerAnim;
    private SwordPickUp swordPickUp;

    private void Start()
    {
        playerAnim = GetComponent<PlayerAnimation>();
        swordPickUp = GetComponent<SwordPickUp>(); 
    }

    private void Update()
    {
        SwingSword();
    }

    private void SwingSword()
    {
        //If Sword is picked up, MB2 is pressed and player is not dead
        if (Input.GetButtonDown("Fire2") && playerState != PlayerState.dead)
        {
            //Return lookdirection
            lookDirecton = playerAnim.GetDirection();

            //If lookdirection is up,  attack up
            if (lookDirecton == LookDirection.up)
            {
                //Disable sword if not already disabled
                TurnSwordOff();
                //Enable sword
                sword[0].SetActive(true);
                //Disable sword after 0.2 seconds
                Invoke("TurnSwordOff", 0.2f);

                //Draw sword hitbox
                RaycastHit2D hit2D;
                hit2D = Physics2D.BoxCast(transform.position + boxOffset[0], boxSize[0], 0, Vector2.zero, 0, layerToHit);
                //If collision is detected
                if (hit2D.collider != null)
                {
                    //Change enemy health
                    hit2D.collider.GetComponent<EnemyHealth>().ChangeHealth(-1);
                }
            }
            //If lookdirection is left,  attack left
            else if (lookDirecton == LookDirection.left)
            {
                //Disable sword if not already disabled
                TurnSwordOff();
                //Enable sword
                sword[1].SetActive(true);
                //Disable sword after 0.2 seconds
                Invoke("TurnSwordOff", 0.2f);

                //Draw sword hitbox
                RaycastHit2D hit2D;
                hit2D = Physics2D.BoxCast(transform.position + boxOffset[1], boxSize[1], 0, Vector2.zero, 0, layerToHit);
                //If collision is detected
                if (hit2D.collider != null)
                {
                    //Change enemy health
                    hit2D.collider.GetComponent<EnemyHealth>().ChangeHealth(-1);
                }
            }
            //If lookdirection is right,  attack right
            else if (lookDirecton == LookDirection.right)
            {
                //Disable sword if not already disabled
                TurnSwordOff();
                //Enable sword
                sword[2].SetActive(true);
                //Disable sword after 0.2 seconds
                Invoke("TurnSwordOff", 0.2f);

                //Draw sword hitbox
                RaycastHit2D hit2D;
                hit2D = Physics2D.BoxCast(transform.position + boxOffset[2], boxSize[2], 0, Vector2.zero, 0, layerToHit);
                //If collision is detected
                if (hit2D.collider != null)
                {
                    //Change enemy health
                    hit2D.collider.GetComponent<EnemyHealth>().ChangeHealth(-1);
                }
            }
            //If lookdirection is right,  attack right
            else if (lookDirecton == LookDirection.down)
            {
                //Disable sword if not already disabled
                TurnSwordOff();
                //Enable sword
                sword[3].SetActive(true);
                //Disable sword after 0.2 seconds
                Invoke("TurnSwordOff", 0.2f);

                //Draw sword hitbox
                RaycastHit2D hit2D;
                hit2D = Physics2D.BoxCast(transform.position + boxOffset[3], boxSize[3], 0, Vector2.zero, 0, layerToHit);
                //If collision is detected
                if (hit2D.collider != null)
                {
                    //Change enemy health
                    hit2D.collider.GetComponent<EnemyHealth>().ChangeHealth(-1);
                }
            }
            playerAnim.GetComponent<Animator>().SetBool("Attack", true);
        }
    }

    //Disables sword gameobject
    private void TurnSwordOff()
    {
        for (int i = 0; i < sword.Length; i++)
        {
            sword[i].SetActive(false);
        }
        playerAnim.GetComponent<Animator>().SetBool("Attack", false);
    }
}
