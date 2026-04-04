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

    public void EnterDangerZone()
    {
        inDangerZone = true;
        threatCleared = false;

        statusText.text = "Entered a Dangerous Zone!!!!";
    }

    public void ExitDangerZone()
    {
        inDangerZone = false;
        threatCleared = true;

        statusText.text = "Safe Zone..";
    }
}