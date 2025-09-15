using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Dinosaur;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class HeadRig : MonoBehaviour
{
    public DinoContext ctx;
    public Rig rig;
    public GameObject targetSmoother;

    public Vector3 dir;
    public float TurnRate { get; set; } = 1f;

    // Start is called before the first frame update
    void Start()
    {
        ctx = GetComponentInParent<DinoController>().dinoContext;
        rig = GetComponent<Rig>();
    }

    public void SetTargetLocation(Vector3 position)
    {
        dir = new Vector3(position.x - ctx.Self.position.x, 10f, position.z - ctx.Self.position.z);
        dir.Normalize();
    }
    // Update is called once per frame
    void Update()
    {
        ctx.LookTarget.transform.position = ctx.Self.position + dir * 10f;
    }


}
