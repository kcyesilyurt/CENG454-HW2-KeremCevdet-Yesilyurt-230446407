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

        examManager.EnterDangerZone();

        activeCountdown = StartCoroutine(MissileCountdown(collision.transform));
    }

    private void OnTriggerExit(Collider collision)
    {
        if (!collision.CompareTag("Player")) return;

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
        yield return new WaitForSeconds(missileDelay);
        missileLauncher.Launch(target);
    }
}