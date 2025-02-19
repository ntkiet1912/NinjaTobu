using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePatrolSquare : MonoBehaviour
{
    [Header("Settings")]
    public float speed = 2f;
    public Transform[] wayPoints;
    [SerializeField] private int currentPoint = 0 ;
    private void Start()
    {
        transform.position = wayPoints[currentPoint].position;
    }
    private void Update()
    {
        if(wayPoints.Length == 0) return;
        transform.position = Vector3.MoveTowards(transform.position, wayPoints[currentPoint].position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, wayPoints[currentPoint].position) < 0.1f)
        {
            currentPoint = (currentPoint + 1) % wayPoints.Length;
        }
    }
}
