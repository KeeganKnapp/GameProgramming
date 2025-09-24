using System.Collections;
using System.Collections.Generic;
using Codice.Client.BaseCommands.Merge.FsLock;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.Dinosaur
{
    public class DinoSensors : MonoBehaviour
    {

        [Header("Sight Settings")]
        [SerializeField] Transform eyes;
        [SerializeField] float sightRange = 25f;
        [SerializeField] float fovDegrees = 110f;

        Vector3 rightBoundary;
        Vector3 leftBoundary;

        private DinoContext ctx;
        private Transform player = null;

        bool playerWithinView, playerNotHidden;

        public bool CanSeePlayer => playerWithinView && playerNotHidden;
        // lower

        // Start is called before the first frame update
        void Start()
        {
            ctx = GetComponent<DinoController>().dinoContext;
            player = ctx.Player;
        }

        // Update is called once per frame
        void Update()
        {
            rightBoundary = Quaternion.Euler(0, fovDegrees / 2, 0) * eyes.forward;
            leftBoundary = Quaternion.Euler(0, -fovDegrees / 2, 0) * eyes.forward;
            if (Vector3.Distance(eyes.position, player.position) <= sightRange)
            {
                Vector3 directionToPlayer = (player.position - eyes.position).normalized;
                float angleToPlayer = Vector3.Angle(eyes.forward, directionToPlayer);
                if (angleToPlayer <= fovDegrees / 2)
                {
                    playerWithinView = true;
                    playerNotHidden = true;
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(eyes.position, eyes.position + eyes.forward * 10);

            //draw cone with radius of sightRange and angle of fovDegrees
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(eyes.position, eyes.position + rightBoundary * sightRange);
            Gizmos.DrawLine(eyes.position, eyes.position + leftBoundary * sightRange);
            Gizmos.DrawWireSphere(eyes.position, sightRange);

            //draw line to player if within sight range and fov
            if (Vector3.Distance(eyes.position, player.position) <= sightRange)
            {
                Vector3 directionToPlayer = (player.position - eyes.position).normalized;
                float angleToPlayer = Vector3.Angle(eyes.forward, directionToPlayer);
                if (angleToPlayer <= fovDegrees / 2)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(eyes.position, player.position);
                }
            }
        }
    }
}