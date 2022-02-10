using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedOctorok : Enemy
{
    [SerializeField] private int maxDistance = 4;
    [SerializeField] private LayerMask layerMask;

    protected override void Start()
    {
        base.Start();
        SetNextDestination();
        //SetDestination(rigidbody2D.position + UnityEngine.Vector2.right * 2);
    }

    private void SetNextDestination()
    {
        LookDirection randomDirection = (LookDirection)Random.Range(0, 4);

        Vector2 directionVector = DirectionToVector2 (randomDirection);

        int randomDistance = Random.Range(1, maxDistance + 1);

        Vector2 destination = rigidbody2D.position + directionVector * randomDistance;

        RaycastHit2D hit = Physics2D.Raycast(rigidbody2D.position, directionVector, randomDistance, layerMask);

        if (hit.collider != null)
        {
            Debug.Log($"{hit.collider.name} is in the way");
            float distanceToHit = Vector2.Distance(rigidbody2D.position, hit.point);

            if (distanceToHit >= 1)
            {
                float newDistance = distanceToHit - distanceToHit % 1;
                destination = rigidbody2D.position + directionVector * newDistance;
            }
            else
            {
                destination = rigidbody2D.position;
            }
        }
        if (!IsInsideViewPort(destination)) 
        { 
            destination = rigidbody2D.position;
        }
        Debug.Log(destination);

        SetDestination(destination); 
    }

    protected override void ReachedDestination()
    {
        base.ReachedDestination();
        Invoke("SetNextDestination", timePerTile);
    }

}
