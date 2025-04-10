using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    private Vector3 offset;
    public float smoothTime = 5;
    void Start()
    {
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetCameraPosition = target.position + offset;
        transform.position = Vector3.Lerp(
            transform.position, 
            targetCameraPosition, 
            Time.deltaTime * smoothTime);
    }
}
