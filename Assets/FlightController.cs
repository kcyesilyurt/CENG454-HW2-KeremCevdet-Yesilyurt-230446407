using UnityEngine;

public class FlightController : MonoBehaviour
{
    public float speed = 9f;
    public float rotationSpeed = 100f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        
        float pitch = 0f;
        if (Input.GetKey(KeyCode.UpArrow)) pitch = 1f;
        if (Input.GetKey(KeyCode.DownArrow)) pitch = -1f;

        
        float yaw = 0f;
        if (Input.GetKey(KeyCode.LeftArrow)) yaw = -1f;
        if (Input.GetKey(KeyCode.RightArrow)) yaw = 1f;
 
        float roll = 0f;
        if (Input.GetKey(KeyCode.Q)) roll = 1f;
        if (Input.GetKey(KeyCode.E)) roll = -1f;

        transform.Rotate(
            pitch * rotationSpeed * Time.deltaTime,
            yaw * rotationSpeed * Time.deltaTime,
            roll * rotationSpeed * Time.deltaTime
        );

        if (Input.GetKey(KeyCode.Space))
        {
        rb.linearVelocity = transform.forward * speed;
        }
        else
        {
        rb.linearVelocity = Vector3.zero;
        }
    }
}