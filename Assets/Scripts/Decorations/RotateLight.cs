using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLight : MonoBehaviour
{
    [Header("Light Rotation")]
    public Transform pivotPoint; 
    public float swingAngle = 20f; 
    public float swingSpeed = 3f; 

    void Update()
    {
        float angle = Mathf.Sin(Time.time * swingSpeed) * swingAngle;
        transform.RotateAround(pivotPoint.position, Vector3.forward, angle - transform.eulerAngles.z);
    }
}
