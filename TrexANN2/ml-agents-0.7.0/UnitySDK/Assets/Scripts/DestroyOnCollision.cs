using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("CollisionEnter triggered");
        if (col.collider.tag == "Agent")
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("TriggerEnter triggered");
        if (other.tag == "Agent" && other.gameObject.activeSelf)
        {
            Destroy(this.gameObject);
        }
    }
}
