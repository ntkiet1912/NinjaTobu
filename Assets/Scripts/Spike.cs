
using UnityEngine;

public class Spike : MonoBehaviour
{
    public Transform cameraTransform;
    private float previousCameraY;


    private void Start()
    {
        previousCameraY = cameraTransform.position.y;
    }
    private void Update()
    {
        float deltaY = cameraTransform.position.y - previousCameraY;
        transform.position += new Vector3(0, deltaY, 0);
        previousCameraY = cameraTransform.position.y;
    }
}
