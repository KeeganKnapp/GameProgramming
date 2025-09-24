using UnityEngine;

public class LookTarget : MonoBehaviour
{
    [Header("References")]
    public Transform characterRoot;      

    [Header("Behavior")]
    public float lookDistance = 10f;    
    public float heightOffset = 1.6f;   
    public float smoothTime = 0.08f;     

    private Vector3 targetWorld;       
    private Vector3 vel;                

    public void SetTargetLocation(Vector3 worldPosition)
    {
        targetWorld = worldPosition;
    }

    void LateUpdate()
    {
        if (!characterRoot) return;

        Vector3 origin = characterRoot.position;
        origin.y += heightOffset;

        Vector3 toTarget = targetWorld - origin;
        toTarget.y = 0f;                        
        if (toTarget.sqrMagnitude < 0.0001f)   
            toTarget = characterRoot.forward;

        Vector3 dir = toTarget.normalized;

        Vector3 desired = origin + dir * lookDistance;

        transform.position = Vector3.SmoothDamp(transform.position, desired, ref vel, smoothTime);
    }
}