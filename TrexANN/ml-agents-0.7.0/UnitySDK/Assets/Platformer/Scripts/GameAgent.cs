using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameAgent : MonoBehaviour
{
    float score;
    int fitness;
    MLP m_net;
    bool alive;
    System.Random rndgen;
    Rigidbody2D rb;
    public Text m_outputText;

    void Start()
    {
        alive = true;
        m_net = new MLP(1, 3, 1);
        fitness = 0;
        score = 0;
        rndgen = new System.Random();
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
    public void UpdateAgent()
    {
        if (alive)
        {
            score += 1 / 60f;
            double distToObstacle = RayCastHorizontal();

            m_net.AddInput(0, (float)distToObstacle);
            fitness = (int)score;
            //Add check distance to ground
            float val = Mathf.Abs(RayCast());
            if (m_net.GenerateOutput() > 0.5 && val < 1)
            {
                rb.AddForce(new Vector2(0, 35   ));
            }
            m_outputText.text = "Network output: " + m_net.m_out.ToString();
        }
        else
        {
            fitness = (int)score;
            score = 0f;
            this.gameObject.SetActive(false);
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
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.down);

        foreach (RaycastHit2D h in hits)
        {
            if (h.collider != null && h.collider.tag == "ground")
            {
                return Vector2.Distance(transform.position, h.transform.position);
            }
        }

        //If not hit, return near infinite value of Integer max bounds
        return 0;
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
