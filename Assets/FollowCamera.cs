using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private Transform target;

    public Vector3 offset = new Vector3(0, 5, -10);

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
    }

    void LateUpdate()
    {
        if (target == null) return;

        transform.position = target.position + offset;
        transform.LookAt(target);
    }
}