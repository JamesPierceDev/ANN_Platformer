using MLAgents;
using UnityEngine;
using UnityEngine.UI;

public class PlatformerAgent : Agent
{
    //public GameObject obstacle;
    private Rigidbody2D rb;
    private BoxCollider2D col;
    private Collider2D m_obstacle;
    public float timeBetweenDecisionsAtInference;
    private float timeSinceDecision;
    private Transform startPosition;
    private float jumpValue = 50;
    private float jumpThreshold = 5;
    bool grounded = true;

    public override void InitializeAgent()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        startPosition = this.transform;

        //Initialise jump height and threshold to random values
        jumpValue = Random.Range(50, 100);
        jumpThreshold = Random.Range(5, 25);
    }

    public override void CollectObservations()
    {
        //Distance from obstacle to jump at
        AddVectorObs(jumpThreshold);
        //How high to jump
        AddVectorObs(jumpValue);
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        if (brain.brainParameters.vectorActionSpaceType == SpaceType.continuous)
        {
            //Jump threshold
            var actionX = vectorAction[0];
            //Jump height
            var actionY = vectorAction[1];

            //If on the ground
            if (RayCast() < 1)
            {
               // Debug.Log("Action Y" + actionY);
               // rb.AddForce(new Vector2(0, actionY * 50));
            }

            Debug.Log("Threshold: " + actionX * 10);
            if (RayCastHorizontal() < Mathf.Abs(actionX * 10))
            {
                rb.AddForce(new Vector2(0, 150));
            }

            //If he jumps off the screen
            if (RayCast() > 30)
            {
                AddReward(-1f);
                Done();
            }

            //If trex somehow falls off
            if (this.transform.position.y > 10)
            {
                AddReward(-1f);
                //Done();
            }
        }
    }

    /// <summary>
    /// Casts a ray directly down from the Agent.
    /// Returns the distance to the obstacle it hits(if any).
    /// If nothing is hit, returns Integer max bounds
    /// </summary>
    /// <returns></returns>
    float RayCast()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);

        if (hit.collider != null && hit.collider.tag == "ground")
        {
            return Vector2.Distance(transform.position, hit.transform.position);
        }

        //If not hit, return near infinite value of Integer max bounds
        return (float)int.MaxValue;
    }

    float RayCastHorizontal()
    {
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, Vector2.right);

        foreach(RaycastHit2D h in hit)
        {
            if (h.collider != null && h.collider.tag == "Cactus")
            {
                return Vector2.Distance(transform.position, h.transform.position);
            }
        }

        return (float)int.MaxValue;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Cactus")
        {
            AddReward(-1f);
            Done();
        }
        if (collision.collider.tag == "ClearanceVolume")
        {
            Debug.Log("Hit clearance volume");
            AddReward(1f);
           // Done();
        }
    }

    public override void AgentReset()
    {
        //Set agent back to starting position
        transform.position = startPosition.position;
    }

}