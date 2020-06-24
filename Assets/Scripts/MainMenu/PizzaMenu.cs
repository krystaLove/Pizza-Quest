using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaMenu : MonoBehaviour
{
    public float rotationSpeed = 1.0f;
    private Transform _transform;
    void Start()
    {
        _transform = gameObject.GetComponent<Transform>();
    }
        
    private void FixedUpdate()
    {
        _transform.Rotate(new Vector3(0f, 0f, 1.0f * rotationSpeed));
    }
}
