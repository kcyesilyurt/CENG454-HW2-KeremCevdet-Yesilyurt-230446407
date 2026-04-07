using UnityEngine;

public class TakeoffAreaController : MonoBehaviour
{
    [SerializeField] private FlightExamManager examManager;
    private bool takeoffRegistered = false;

    private void OnTriggerExit(Collider other)
    {
        if (takeoffRegistered) return;
        if (!other.CompareTag("Player")) return;

        takeoffRegistered = true;
        examManager.RegisterTakeoff();
    }
}