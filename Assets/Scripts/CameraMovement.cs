using Cinemachine;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    private CinemachineBrain cinemachineBrain;
    public Transform Player;
    private Vector3 initialPlayerPosition;
    private bool isGameStarted = false;

    void Start()
    {
        cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();
        initialPlayerPosition = Player.position;
    }

    void Update()
    {
        if(CheckIsGameStarted())
        {
            Vector3 currentPosition = transform.position;
            currentPosition.y += moveSpeed * Time.deltaTime;
            transform.position = currentPosition;
        }
       
    }
    private bool CheckIsGameStarted()
    {
       if(Mathf.Abs(Player.position.y - initialPlayerPosition.y)>0.1f)
        {
            isGameStarted = true;
        }
        return isGameStarted;
    }

}

