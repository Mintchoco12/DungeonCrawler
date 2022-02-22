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

    private void Start()
    {
        playerAnim = GetComponent<PlayerAnimation>();
    }

    private void Update()
    {
        SwingSword();
    }

    private void SwingSword()
    {
        if (Input.GetButtonDown("Fire2") && playerState != PlayerState.dead)
        {
            lookDirecton = playerAnim.GetDirection();

            if (lookDirecton == LookDirection.up)
            {
                TurnSwordOff();
                sword[0].SetActive(true);
                Invoke("TurnSwordOff", 0.2f);

                RaycastHit2D hit2D;
                hit2D = Physics2D.BoxCast(transform.position + boxOffset[0], boxSize[0], 0, Vector2.zero, 0, layerToHit);
                if (hit2D.collider != null)
                {
                    print(hit2D.collider.name);
                    hit2D.collider.GetComponent<EnemyHealth>().ChangeHealth(-1);
                }
            }
            else if (lookDirecton == LookDirection.left)
            {
                TurnSwordOff();
                sword[1].SetActive(true);
                Invoke("TurnSwordOff", 0.2f);

                RaycastHit2D hit2D;
                hit2D = Physics2D.BoxCast(transform.position + boxOffset[1], boxSize[1], 0, Vector2.zero, 0, layerToHit);
                if (hit2D.collider != null)
                {
                    print(hit2D.collider.name);
                    hit2D.collider.GetComponent<EnemyHealth>().ChangeHealth(-1);
                }
            }
            else if (lookDirecton == LookDirection.right)
            {
                TurnSwordOff();
                sword[2].SetActive(true);
                Invoke("TurnSwordOff", 0.2f);

                RaycastHit2D hit2D;
                hit2D = Physics2D.BoxCast(transform.position + boxOffset[2], boxSize[2], 0, Vector2.zero, 0, layerToHit);
                if (hit2D.collider != null)
                {
                    print(hit2D.collider.name);
                    hit2D.collider.GetComponent<EnemyHealth>().ChangeHealth(-1);
                }
            }
            else if (lookDirecton == LookDirection.down)
            {
                TurnSwordOff();
                sword[3].SetActive(true);
                Invoke("TurnSwordOff", 0.2f);

                RaycastHit2D hit2D;
                hit2D = Physics2D.BoxCast(transform.position + boxOffset[3], boxSize[3], 0, Vector2.zero, 0, layerToHit);
                if (hit2D.collider != null)
                {
                    print(hit2D.collider.name);
                    hit2D.collider.GetComponent<EnemyHealth>().ChangeHealth(-1);
                }
            }
            playerAnim.GetComponent<Animator>().SetBool("Attack", true);
        }
    }

    private void TurnSwordOff()
    {
        for (int i = 0; i < sword.Length; i++)
        {
            sword[i].SetActive(false);
        }
        playerAnim.GetComponent<Animator>().SetBool("Attack", false);
    }
}
