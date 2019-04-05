using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapController : MonoBehaviour {

    public GameObject[] m_tiles = new GameObject[3];
    private Vector2 m_size;
    private Vector2 m_initial;
    private Vector2 m_final;
    private BoxCollider2D bc;
    public Text m_scoreText;
    private float m_speed;
    private float m_score;
    private float timer;
    
	/// <summary>
    /// 
    /// </summary>
	void Start () {
        Debug.Log("Mapcontroller -> Start");
        timer = 0;
        m_initial = m_tiles[0].transform.position;
        m_final = m_tiles[m_tiles.GetUpperBound(0)].transform.position;
        bc = m_tiles[0].GetComponent<BoxCollider2D>();
        m_size.x = bc.size.x;
        m_size.y = bc.size.y;
        m_speed = -0.1f;

        Debug.Log("Initial position: " + m_initial.x + " : " + m_initial.y);
    }
	
	/// <summary>
    /// 
    /// </summary>
	void Update () {
        for (byte i = 0; i < m_tiles.Length; i++)
        {
            if (m_tiles[i].transform.position.x < 0 - m_size.x)
            {
                m_tiles[i].transform.position = m_final;
            }

            m_tiles[i].transform.Translate(new Vector3(m_speed, 0, 0));
        }

        if (timer > 0.2)
        {
            m_score++;
            timer = 0;
        }
        timer += 1 / 60f;
        m_scoreText.text = "Score: " + m_score.ToString();
    }
}