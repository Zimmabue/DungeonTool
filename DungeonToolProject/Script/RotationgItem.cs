using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationgItem : MonoBehaviour {

    public int speed;
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, 0,- (Time.deltaTime*speed));
	}
}
