using UnityEngine;

public class MissileLauncher : MonoBehaviour
{
    [SerializeField] private GameObject missilePrefab;
    [SerializeField] private Transform launchPoint;
    [SerializeField] private AudioSource launchAudioSource;

    private GameObject activeMissile;

    public GameObject Launch(Transform target)
    {
        Debug.Log("Launch called");
        if(missilePrefab == null)
        {
            Debug.Log("missileprefab is null");
            return null;
        }
        if (launchPoint == null)
        {
            Debug.Log("launchpoint is null");
            return null;
        }

        activeMissile = Instantiate(missilePrefab, launchPoint.position, launchPoint.rotation);
        Debug.Log("Missile instantiated: " + activeMissile.name);

        MissileHoming homing = activeMissile.GetComponent<MissileHoming>();
        if (homing != null)
        {
            homing.SetTarget(target);
            Debug.Log("Target assigned");
        }

        if (launchAudioSource != null)
        {
            launchAudioSource.Play();
        }

        return activeMissile;
    }

    public void DestroyActiveMissile()
    {
        if (activeMissile != null)
        {
            Destroy(activeMissile);
            activeMissile = null;
        }
    }
}