using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAgent : MonoBehaviour {

    float score;
    int fitness;
    MLP m_net;
    bool alive;
    bool jump = false;
    private Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        Debug.Log("GameAgent -> Start");
        m_net = new MLP(1, 3, 1);
        fitness = 0;
        score = 0;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    public int GetFitness()
    {
        return fitness;
    }

    public void SetNetwork(MLP n)
    {
        m_net = n;
    }

    public MLP GetNetwork()
    {
        return m_net;
    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            score += 1 / 60f;

            float distToObstacle = Mathf.Abs(RayCastHorizontal());
            m_net.AddInput(0, distToObstacle);

            //Add check distance to ground
            if (m_net.GenerateOutput() > 0.5)
            {
                Debug.Log("Network output: " + m_net.m_out);
                jump = true;
            }

            if (jump && RayCast() < 1)
            {
                rb.AddForce(new Vector2(0, 150));
            }
        }
        else
        {
            fitness = (int)score;
            score = 0f;
            Reset();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void Reset()
    {
        alive = true;
        score = 0;
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

        foreach (RaycastHit2D h in hit)
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
            alive = false;
        }
    }
}
