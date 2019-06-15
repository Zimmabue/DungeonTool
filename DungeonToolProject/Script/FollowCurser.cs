using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCurser : MonoBehaviour {


    public TrailRenderer trail;

	// Use this for initialization
	void Start () {
        trail = GetComponent<TrailRenderer>();
        Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            trail.startColor = Color.yellow;
            
        }
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = pos;
	}
}
