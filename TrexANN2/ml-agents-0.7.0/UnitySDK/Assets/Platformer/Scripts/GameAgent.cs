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
    public bool initialised = false;
    public Slider timeSlider;

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
        Time.timeScale = timeSlider.value;
        if (gameObject.activeSelf)
        {
            score += 1 / 60f;
            double distToObstacle = RayCastHorizontal();
            if (distToObstacle > 0)
            {
                initialised = true;
                m_net.AddInput(0, (float)distToObstacle);
                fitness = (int)score;
                //Add check distance to ground
                float val = Mathf.Abs(RayCast());
                if (m_net.GenerateOutput() > 0.5 && val < 1)
                {
                    Debug.Log("Jump");
                    rb.AddForce(new Vector2(0, 150));
                }
                m_outputText.text = "Network output: " + Mathf.Round(m_net.m_out * 100) / 100;
            }
        }
        else
        {
            m_outputText.text = "Network output: 0";
            fitness = (int)score;
            score = 0f;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void Reset()
    {
        gameObject.SetActive(true);
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
        return 50;
    }

    float RayCastHorizontal()
    {
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, Vector2.right, Mathf.Infinity);

        foreach (RaycastHit2D h in hit)
        {
            if (h.collider != null && h.collider.tag == "Cactus")
            {
                return Vector2.Distance(transform.position, h.transform.position);
            }
        }
        return 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Cactus")
        {
            gameObject.SetActive(false);
        }

        if (collision.collider.tag == "FallVolume")
        {
            gameObject.SetActive(false);
        }
    }
}
