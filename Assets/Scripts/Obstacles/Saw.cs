using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Saw : MonoBehaviour
{
    public float rotateSpeed = 200f;
    private void Update()
    {
        transform.Rotate(0, 0, -rotateSpeed * Time.deltaTime);
    }
    
}
