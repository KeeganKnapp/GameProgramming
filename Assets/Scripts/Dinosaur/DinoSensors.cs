using System.Collections;
using System.Collections.Generic;
using Codice.Client.BaseCommands.Merge.FsLock;
using UnityEngine;

namespace Assets.Scripts.Dinosaur
{
    public class DinoSensors : MonoBehaviour
    {
        [SerializeField] Transform eyes;
        [SerializeField] float sightRange = 25f;
        [SerializeField] float fovDegrees = 110f;
        private DinoContext ctx;
        private Transform player;

        bool isInAngle, isInRange, isNotHidden = false;

        public bool SeesPlayer;
        // lower

        // Start is called before the first frame update
        void OnEnable() {
        }
        void Start()
        {
            ctx = GetComponent<DinoController>().dinoContext;
            player = ctx.Player;
        }

        // Update is called once per frame
        void Update()
        {
            //player in range
            if (Vector3.Distance(transform.position, player.position) <= sightRange) isInRange = true;
            //player hidden
            RaycastHit hit;
            if (Physics.Raycast(transform.position, (player.position - transform.position), out hit, Mathf.Infinity))
            {
                if (hit.transform == player.transform)
                {
                    isNotHidden = true;
                }
            }
            //player in view angle
            Vector3 selfToPlayer = player.transform.position - transform.position;
            Vector3 forward = transform.forward;
            float angle = Vector3.SignedAngle(selfToPlayer, forward, Vector3.up);
            if (angle < fovDegrees/2 && angle < -1 * fovDegrees/2)
            {
                isInAngle = true;
            }

            SeesPlayer = isNotHidden && isInAngle && isInRange;
        }

        private void OnDrawGizmos()
        {
        }

        public bool canSeePlayer()
        {
            return false;
        }
    }
}