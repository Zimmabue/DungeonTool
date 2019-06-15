using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

    public float zDistance;
    public float speed;
    public Transform target { get; set; }

    private Transform myTransform;

    private void Start()
    {
        myTransform = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;

        var position = target.position + Vector3.back * zDistance;
        myTransform.position = Vector3.Lerp(myTransform.position, position, Time.deltaTime * speed);
    }
}
