//FlightExamManager.cs
using UnityEngine;
using TMPro;
using System.Collections;

public class FlightExamManager : MonoBehaviour
{
    private enum ExamState
    {
        WaitingForTakeoff,
        ReadyForDangerZone,
        DangerZoneCountdown,
        MissileActive,
        ThreatCleared,
        MissionComplete
    }

    [SerializeField] private TMP_Text statusText;
    [SerializeField] private TMP_Text missionText;
    [SerializeField] private AudioSource successAudioSource;

    private ExamState currentState = ExamState.WaitingForTakeoff;
    private Coroutine statusRoutine;

    public bool IsPlayerInDangerZone { get; private set; }

    private void Start()
    {
        if (statusText != null)
            statusText.text = "";

        UpdateMissionText();
    }

    private void SetState(ExamState newState)
    {
        currentState = newState;
        UpdateMissionText();
    }

    private void UpdateMissionText()
    {
        if (missionText == null) return;

        switch (currentState)
        {
            case ExamState.WaitingForTakeoff:
                missionText.text = "Take off first.";
                break;

            case ExamState.ReadyForDangerZone:
                missionText.text = "Enter the danger zone.";
                break;

            case ExamState.DangerZoneCountdown:
                missionText.text = "Missile countdown active. Stay alert.";
                break;

            case ExamState.MissileActive:
                missionText.text = "Missile active. Escape the danger zone.";
                break;

            case ExamState.ThreatCleared:
                missionText.text = "Threat cleared. Land safely.";
                break;

            case ExamState.MissionComplete:
                missionText.text = "Successful landing completed.";
                break;
        }
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
        if (currentState != ExamState.WaitingForTakeoff) return;

        SetState(ExamState.ReadyForDangerZone);
        SetTemporaryStatusMessage("Takeoff registered.", 2f);
    }

    public bool EnterDangerZone()
    {
        if (currentState != ExamState.ReadyForDangerZone) return false;

        IsPlayerInDangerZone = true;
        SetState(ExamState.DangerZoneCountdown);
        SetStatusMessage("Entered Dangerous Zone!");
        return true;
    }

    public bool CanLaunchMissile()
    {
        return currentState == ExamState.DangerZoneCountdown && IsPlayerInDangerZone;
    }

    public void NotifyMissileLaunched()
    {
        if (!CanLaunchMissile()) return;

        SetState(ExamState.MissileActive);
        SetStatusMessage("Missile launched!");
    }

    public void ExitDangerZone()
    {
        if (!IsPlayerInDangerZone) return;

        IsPlayerInDangerZone = false;

        if (currentState == ExamState.DangerZoneCountdown)
        {
            SetState(ExamState.ReadyForDangerZone);
            SetTemporaryStatusMessage("Exited too early. Re-enter the danger zone.", 3f);
            return;
        }

        if (currentState == ExamState.MissileActive)
        {
            SetState(ExamState.ThreatCleared);
            SetTemporaryStatusMessage("Safe zone reached.", 5f);
        }
    }

    public void FailThreatPhase()
    {
        IsPlayerInDangerZone = false;

        if (currentState != ExamState.DangerZoneCountdown &&
            currentState != ExamState.MissileActive)
        {
            return;
        }

        SetState(ExamState.ReadyForDangerZone);
        SetStatusMessage("Missile hit! Return and try again.");
    }

    public void TryCompleteLanding()
    {
        if (currentState == ExamState.MissionComplete) return;

        if (currentState == ExamState.WaitingForTakeoff)
        {
            SetTemporaryStatusMessage("Take off first.", 3f);
            return;
        }

        if (currentState == ExamState.ReadyForDangerZone ||
            currentState == ExamState.DangerZoneCountdown ||
            currentState == ExamState.MissileActive)
        {
            SetTemporaryStatusMessage("Clear the danger zone before landing.", 3f);
            return;
        }

        if (currentState != ExamState.ThreatCleared)
        {
            SetTemporaryStatusMessage("Landing not allowed yet.", 3f);
            return;
        }
        if(successAudioSource != null)
        {
            successAudioSource.Play();
        }

        SetState(ExamState.MissionComplete);
        SetStatusMessage("Mission Complete!");
    }
}