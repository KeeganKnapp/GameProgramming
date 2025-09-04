using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GravityProbe : MonoBehaviour
{
    Rigidbody rb;
    void Awake() { rb = GetComponent<Rigidbody>(); rb.useGravity = true; rb.isKinematic = false; }
    void Update() { Debug.Log($"vy = {rb.velocity.y:F3} positionY = {transform.position.y:F3}"); }
}