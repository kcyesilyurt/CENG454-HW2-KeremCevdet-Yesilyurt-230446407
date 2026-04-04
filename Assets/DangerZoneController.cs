using UnityEngine;
using System.Collections;

public class DangerZoneController : MonoBehaviour
{
    [SerializeField] private FlightExamManager examManager;
    [SerializeField] private MissileLauncher missileLauncher;
    [SerializeField] private float missileDelay = 5f;

    private Coroutine activeCountdown;

    private void OnTriggerEnter(Collider collision)
    {
        if (!collision.CompareTag("Player")) return;
        Debug.Log("Player Entered danger zone");

        examManager.EnterDangerZone();

        activeCountdown = StartCoroutine(MissileCountdown(collision.transform));
    }

    private void OnTriggerExit(Collider collision)
    {
        if (!collision.CompareTag("Player")) return;
        Debug.Log("Player exited danger zone");

        if (activeCountdown != null)
        {
            StopCoroutine(activeCountdown);
            activeCountdown = null;
        }

        missileLauncher.DestroyActiveMissile();
        examManager.ExitDangerZone();
    }

    private IEnumerator MissileCountdown(Transform target)
    {
        Debug.Log("Missile countdown started");
        yield return new WaitForSeconds(missileDelay);
        Debug.Log("countdown finished, launching now:)");
        missileLauncher.Launch(target);
    }
}