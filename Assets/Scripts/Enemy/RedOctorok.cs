using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedOctorok : Enemy
{
    [SerializeField] private int maxDistance;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float chanceToShoot;
    [SerializeField] private LayerMask layerMask;

    

    protected override void Start()
    {
        base.Start();
        SetNextDestination();
    }

    private void SetNextDestination()
    {
        //Generates a random direction (up, down, left, right)
        direction = (LookDirection)Random.Range(0, 4);
        //Converts it to a vector2
        Vector2 directionVector = DirectionToVector2 (direction);
        //Generates random distance (amount of tiles) to walk
        int randomDistance = Random.Range(1, maxDistance + 1);
        //Calculates the destination vector
        Vector2 destination = rigidbody2D.position + directionVector * randomDistance;

        RaycastHit2D hit = Physics2D.Raycast(rigidbody2D.position, directionVector, randomDistance, layerMask);

        //If the path is clear 
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

        //Checks if new position is in the current screen
        if (!IsInsideViewPort(destination)) 
        { 
            destination = rigidbody2D.position;
        }
 
        //Calls SetDestination with new destination
        SetDestination(destination); 
    }

    protected override void ReachedDestination()
    {
        base.ReachedDestination();

        //If Random.value is lower than chanceToShoow
        if (Random.value < chanceToShoot)
        {
            //Attack
            Attack();
        }
        else
        {
            //Else wait and go to next destination
            Invoke("SetNextDestination", timePerTile);
        }
    }

    protected override void Attack()
    {
        base.Attack();

        //Aim towards player
        direction = CalculateDirection(rigidbody2D.position, player.position);

        //Spawn projectile after 15 frames towards enemy(player)
        Invoke("ShootProjectile", Time.deltaTime * 15);

        //After 40 frames go to next destination
        Invoke("SetNextDestination", Time.deltaTime * 40);
    }

    public void ShootProjectile()
    {
        //If projectile isnt in unity, error and stop
        if (projectile == null)
        {
            Debug.Log("No projectile set on " + name);
            return;
        }

        //Instantiate gameobject on octorok
        GameObject proj = Instantiate(projectile, transform.position, Quaternion.identity);

        //Return refernce to script of projectile
        Projectile p = proj.GetComponent<Projectile>();

        //If script isnt found, error and stop
        if (p == null)
        {
            Debug.Log("Projectile not found on instance " + name);
            return;
        }

        p.Launch(gameObject, direction);
    }
}
