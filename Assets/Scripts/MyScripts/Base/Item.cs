using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("Floating")]
    public float floatSpeed = 2f;   
    public float floatHeight = 0.5f;
    private Vector3 startPos;
    
    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        FloatMotion();
    }

    private void FloatMotion()
    {
        float newY = Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = startPos + new Vector3(0, newY, 0);
    }
    
    protected internal virtual void UseItem(GameObject entity)
    {
        Destroy(gameObject);
    }
}
