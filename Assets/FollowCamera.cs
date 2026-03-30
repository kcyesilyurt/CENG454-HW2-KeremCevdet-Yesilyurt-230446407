using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 4f, -8f);

    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
            transform.LookAt(target);
        }
    }
}