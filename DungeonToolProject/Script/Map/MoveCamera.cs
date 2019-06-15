using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour {

    public float speed;
    public float zoomSpeed;
    public float distance_z;
    private Camera mainCamera;

	// Use this for initialization
	void Start ()
    {
        mainCamera = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (mainCamera == null)
            return;

        if (Input.GetMouseButton(2)) {
		  
                transform.position -= Vector3.right * Input.GetAxisRaw("Mouse X") * Time.deltaTime * speed +
                   Vector3.up * Input.GetAxisRaw("Mouse Y") * Time.deltaTime * speed;
                  
        }

        mainCamera.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * zoomSpeed;
        mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize, 1, 18);
	}
}
