using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class ObstableController : MonoBehaviour {

    public GameObject source;
    public GameObject dest;
    private List<GameObject> m_obstacles;
    private Vector2 m_initial;
    private float m_speed;
    float timer;

	// Use this for initialization
	void Start () {
        timer = 0;
        m_initial = new Vector2(dest.transform.position.x, dest.transform.position.y + 0.17f);
        m_obstacles = new List<GameObject>();
        m_speed = -0.1f;
    }
	
	// Update is called once per frame
	void Update () {
        timer += 1 / 60f;

        if (timer > 1.5)
        {
            m_obstacles.Add((GameObject)Instantiate(source, m_initial, Quaternion.identity));
            timer = 0;
        }

        for (byte i = 0; i < m_obstacles.Count; i++)
        {
            if (m_obstacles[i] != null)
            {
                m_obstacles[i].transform.Translate(m_speed, 0, 0);

                Vector2 pos = Camera.main.WorldToViewportPoint(m_obstacles[i].transform.position);
                if (pos.x < 0)
                {
                    Destroy(m_obstacles[i]);
                    m_obstacles.RemoveAt(i);
                }
            }
            else
            {
                m_obstacles.RemoveAt(i);
            }
        }
	}
}