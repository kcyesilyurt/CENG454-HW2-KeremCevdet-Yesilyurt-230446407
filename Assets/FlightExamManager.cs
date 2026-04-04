using UnityEngine;
using TMPro;

public class FlightExamManager : MonoBehaviour
{
    [SerializeField] private TMP_Text statusText;
    [SerializeField] private TMP_Text missionText;

    private bool hasTakenOff = false;
    private bool threatCleared = false;
    private bool missionComplete = false;
    private bool inDangerZone = false;

    private void Start()
    {
        if (statusText != null)
            statusText.text = "";

        if (missionText != null)
            missionText.text = "";
    }

    public void EnterDangerZone()
    {
        inDangerZone = true;
        threatCleared = false;

        if (statusText != null)
            statusText.text = "Entered a Dangerous Zone!";
    }

    public void ExitDangerZone()
    {
        inDangerZone = false;
        threatCleared = true;

        if (statusText != null)
            statusText.text = "Safe Zone";
    }
}