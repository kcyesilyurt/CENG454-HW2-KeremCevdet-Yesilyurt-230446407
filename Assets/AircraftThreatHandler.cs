using UnityEngine;

public class AircraftThreatHandler : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private FlightExamManager examManager;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Missile")) return;

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.position = respawnPoint.position;
            rb.rotation = respawnPoint.rotation;
        }
        else
        {
            transform.position = respawnPoint.position;
            transform.rotation = respawnPoint.rotation;
        }

        if (examManager != null)
        {
            examManager.FailThreatPhase();
        }

        Destroy(other.gameObject);
    }
}