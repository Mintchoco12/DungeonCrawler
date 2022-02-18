using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float timePerTile = 1f;
    protected new Rigidbody2D rigidbody2D;
    private Animator animator;
    private Camera renderCam;
    protected Transform player;

    private EnemyState state;
    protected LookDirection direction;

    private Vector2 startPosition;
    private Vector2 targetPosition;
    private float distanceToLerp;
    private float lerpTimer;
    
    protected virtual void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>().transform;
        renderCam = GameObject.Find("RenderCam").GetComponent<Camera>();
        animator = GetComponent<Animator>();
    }

    protected void SetDestination(Vector2 destination)
    {
        //Saves points for A to B lerp
        startPosition = transform.position;
        targetPosition = destination;

        //Calculates the lerp distance
        distanceToLerp = Vector2.Distance(startPosition, destination);

        //Set timer on 0 and start roaming
        lerpTimer = 0;
        state = EnemyState.roaming;
    }

    private void MoveToDestination()
    {
        //Adds deltatime to lerptimer
        lerpTimer += Time.deltaTime;

        //If lerptimer > distance * time per tile
        if (lerpTimer > (timePerTile * distanceToLerp))
        {
            //Set timer to max
            lerpTimer = timePerTile * distanceToLerp;
        }

        //Divide by 0 check
        if (!distanceToLerp.Equals(0f) && !timePerTile.Equals(0f))
        {
            //Calculates lerp distance
            float perc = lerpTimer / (timePerTile * distanceToLerp);
            rigidbody2D.MovePosition(Vector3.Lerp(startPosition, targetPosition, perc));
        }

        //If lerptimer is done
        if (lerpTimer.Equals(timePerTile * distanceToLerp))
        {
            //The destination is reached
            ReachedDestination();
        }
    }

    protected virtual void ReachedDestination()
    {
        state = EnemyState.idle;
    }

    protected virtual void Attack()
    {
        Debug.Log("Attacking");
        state = EnemyState.attacking;
    }

    private void FixedUpdate()
    {
        UpdateAnimator();

        if (state == EnemyState.roaming)
        {
            MoveToDestination();
        }
    }

    protected Vector2 DirectionToVector2(LookDirection direction)
    {
        Vector2 directionVector = Vector2.zero;

        switch(direction)
        {
            case LookDirection.up:
                directionVector = Vector2.up;
                break;
            case LookDirection.right:
                directionVector = Vector2.right;
                break;
            case LookDirection.down:
                directionVector = Vector2.down;
                break;
            case LookDirection.left:
                directionVector = Vector2.left;
                break;
        }

        return directionVector;
    }

    protected bool IsInsideViewPort(Vector2 position)
    {
        Vector2 viewportPoint = renderCam.WorldToViewportPoint(position);
        return viewportPoint.x > 0f && viewportPoint.x < 1 && viewportPoint.y > 0 && viewportPoint.y < 0.7f;
    }

    private void UpdateAnimator()
    {
        if (animator != null)
        {
            animator.SetInteger("EnemyState", (int)state);
            animator.SetFloat("LookDirection", (float)direction);
        }
        else
        {
            Debug.Log("No animator found on " + name);
        }
    }

    protected float CalculateAngle(Vector3 from, Vector3 to)
    {
        //Calculates the angle degree between two vectors
        return Quaternion.FromToRotation(Vector3.up, to - from).eulerAngles.z;
    }
        
    protected LookDirection CalculateDirection(Vector2 pos1, Vector2 pos2, int amountDirections = 4)
    {
        float angle = 360f - CalculateAngle(pos1, pos2);

        float part = 360f / amountDirections;

        if (angle.Equals(360))
        {
            angle = 0;
        }

        int result = Mathf.RoundToInt(angle / part);

        if (result == amountDirections)
        {
            result = 0;
        }

        LookDirection direction = LookDirection.up;

        switch(result)
        {
            case 1:
                direction = LookDirection.right;
                break;
            case 2:
                direction = LookDirection.left;
                break;
            case 3:
                direction = LookDirection.down;
                break;

        }

        return direction;
    }
}
