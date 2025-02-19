
using UnityEngine;

public class Spike : MonoBehaviour
{
    public Transform cameraTransform;
    private float previousCameraY;
    public GameObject gameOverCanvas;

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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Time.timeScale = 0;
            gameOverCanvas.SetActive(true);
        }
    }
}
