//FlightExamManager.cs
using UnityEngine;
using TMPro;
using System.Collections;

public class FlightExamManager : MonoBehaviour
{
    [SerializeField] private TMP_Text statusText;
    [SerializeField] private TMP_Text missionText;

    private bool hasTakenOff = false;
    private bool hasEnteredDangerZone = false;
    private bool threatCleared = false;
    private bool missionComplete = false;

    private Coroutine statusRoutine;

    private void Start()
    {
        if (statusText != null)
            statusText.text = "";

        if (missionText != null)
            missionText.text = "Take off, survive the danger zone, then land safely.";
    }

    private void SetStatusMessage(string message)
    {
        if (statusRoutine != null)
        {
            StopCoroutine(statusRoutine);
            statusRoutine = null;
        }

        if (statusText != null)
            statusText.text = message;
    }

    private void SetTemporaryStatusMessage(string message, float duration)
    {
        if (statusRoutine != null)
        {
            StopCoroutine(statusRoutine);
            statusRoutine = null;
        }

        statusRoutine = StartCoroutine(TemporaryStatusRoutine(message, duration));
    }

    private IEnumerator TemporaryStatusRoutine(string message, float duration)
    {
        if (statusText != null)
            statusText.text = message;

        yield return new WaitForSeconds(duration);

        if (statusText != null)
            statusText.text = "";

        statusRoutine = null;
    }

    public void RegisterTakeoff()
    {
        hasTakenOff = true;

        if (missionText != null)
            missionText.text = "Enter the danger zone.";
    }

    public void EnterDangerZone()
    {
        hasEnteredDangerZone = true;
        threatCleared = false;

        SetStatusMessage("Entered a Dangerous Zone!");

        if (missionText != null)
            missionText.text = "Survive the missile threat and escape.";
    }

    public void ShowMissileLaunched()
    {
        SetStatusMessage("Missile Launched!");
    }

    public void ExitDangerZone()
    {
        threatCleared = true;

        SetTemporaryStatusMessage("Safe Zone", 5f);

        if (missionText != null)
            missionText.text = "Threat cleared. Land safely.";
    }

    public void FailThreatPhase()
    {
        threatCleared = false;

        SetStatusMessage("Missile Hit!");

        if (missionText != null)
            missionText.text = "Return and try again.";
    }

    public void TryCompleteLanding()
    {
        if (missionComplete) return;

        if (!hasTakenOff)
        {
            SetTemporaryStatusMessage("Take off first.", 3f);
            return;
        }

        if (!hasEnteredDangerZone)
        {
            SetTemporaryStatusMessage("Enter the danger zone first.", 3f);
            return;
        }

        if (!threatCleared)
        {
            SetTemporaryStatusMessage("Clear the threat before landing.", 3f);
            return;
        }

        missionComplete = true;

        SetStatusMessage("Mission Complete!");

        if (missionText != null)
            missionText.text = "Successful landing completed.";
    }
}