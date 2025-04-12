using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform objectA;  // Reference to object A
    public Transform objectB;  // Reference to object B

    public float smoothSpeed = 2f;  // Speed of the camera movement
    public float targetZPositionA = -30f;  // Z position when object A is clicked
    public float targetZPositionB = 0f;   // Z position when object B is clicked

    private Vector3 targetPosition;  // Target position for the camera

    private void Start()
    {
        // Set the initial camera position (can be adjusted as needed)
        targetPosition = transform.position;
    }

    private void Update()
    {
        // Detect mouse click on objects
        if (Input.GetMouseButtonDown(0))  // Left mouse button click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Check if clicked on Object A
                if (hit.transform == objectA)
                {
                    targetPosition = new Vector3(transform.position.x, transform.position.y, targetZPositionA);
                }
                // Check if clicked on Object B
                else if (hit.transform == objectB)
                {
                    targetPosition = new Vector3(transform.position.x, transform.position.y, targetZPositionB);
                }
            }
        }

        // Smoothly move the camera to the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}
