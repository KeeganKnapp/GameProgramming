using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Camera cam;
    public Transform body; 

    [Header("Movement")]
    public float moveSpeed = 6f;

    [Header("Mouse Look")]
    public float sensitivity = 200f;
    public float pitchClamp = 80f;

    float yaw; 
    float pitch;

    void Awake()
    {

    }

    void Start()
    {

    }

    void Update()
    {

    }
}