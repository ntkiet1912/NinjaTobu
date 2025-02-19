using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateVision : MonoBehaviour
{
    public Transform pivotPoint; // Đỉnh cố định của chóp
    public float swingAngle = 20f; // Góc dao động tối đa
    public float swingSpeed = 3f; // Tốc độ dao động

    void Update()
    {
        float angle = Mathf.Sin(Time.time * swingSpeed) * swingAngle;
        transform.RotateAround(pivotPoint.position, Vector3.forward, angle - transform.eulerAngles.z);
    }
}
