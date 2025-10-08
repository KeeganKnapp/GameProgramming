using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Dinosaur
{
    public class DinoSensors : MonoBehaviour
    {
        [Header("Sight Settings")]
        [SerializeField] Transform eyes;
        [SerializeField] public float sightRange = 25f;
        [SerializeField] float fovDegrees = 110f;

        [Header("Suspicion Settings")]
        float suspicionHalfLife = 1000f;
        float startSuspicionRadius = 30f;
        float confidenceIncrement = 0.005f;
        List<SuspiciousLocation> suspiciousLocations;
        Vector3 rightBoundary;
        Vector3 leftBoundary;

        private DinoContext ctx;
        private GameObject player = null;

        public bool CanSeePlayer => CheckSight() && CheckNotHidden();

        // Start is called before the first frame update
        void Start()
        {
            ctx = GetComponent<DinoController>().dinoContext;
            player = ctx.PlayerObject;
            suspiciousLocations = ctx.SuspiciousLocations;
        }

        // Update is called once per frame
        void Update()
        {
            if (CanSeePlayer)
            {
                AddSuspicion();
            }

            foreach (var loc in suspiciousLocations)
            {
                loc.Decay(suspicionHalfLife);
            }

            suspiciousLocations.RemoveAll(loc => loc.Confidence < 0.1f);
        }

        void AddSuspicion()
        {
            // Find nearest existing suspicion
            SuspiciousLocation nearest = null;
            float nearestDist = float.MaxValue;

            foreach (var loc in suspiciousLocations)
            {
                float dist = Vector3.Distance(loc.Center, player.transform.position);
                if (dist < nearestDist)
                {
                    nearestDist = dist;
                    nearest = loc;
                }
            }

            // If within radius of existing suspicion, reinforce it
            if (nearest != null && nearestDist < nearest.Radius)
            {
                nearest.Reinforce(player.transform.position, confidenceIncrement);
            }
            else // Add new location if not one nearby
            {
                float dist = Vector3.Distance(eyes.position, player.transform.position);
                float distFactor = Mathf.InverseLerp(sightRange, 0f, dist);           // closer -> higher
                Vector3 dir = (player.transform.position - eyes.position).normalized;
                float ang  = Vector3.Angle(eyes.forward, dir);
                float facingFactor = (ang <= fovDegrees * 0.5f) ? 1.0f : 0.7f;        // small boost if centered
                float initialConf = Mathf.Clamp01(0.25f + 0.6f * distFactor) * facingFactor;

                suspiciousLocations.Add(new SuspiciousLocation(player.transform.position, SuspicionType.Sight)
                {
                    Radius = startSuspicionRadius,
                    Confidence = initialConf
                });
            }
        }

        bool CheckSight()
        {
            rightBoundary = Quaternion.Euler(0, fovDegrees / 2, 0) * eyes.forward;
            leftBoundary = Quaternion.Euler(0, -fovDegrees / 2, 0) * eyes.forward;

            if (Vector3.Distance(eyes.position, player.transform.position) <= sightRange)
            {
                Vector3 directionToPlayer = (player.transform.position - eyes.position).normalized;
                float angleToPlayer = Vector3.Angle(eyes.forward, directionToPlayer);
                if (angleToPlayer <= fovDegrees / 2)
                {
                    return true;
                }
            }

            return false;
        }

        bool CheckNotHidden()
        {
            // Return true for now
            return true;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(eyes.position, eyes.position + eyes.forward * 10);

            // Draw cone with radius of sightRange and angle of fovDegrees
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(eyes.position, eyes.position + rightBoundary * sightRange);
            Gizmos.DrawLine(eyes.position, eyes.position + leftBoundary * sightRange);
            Gizmos.DrawWireSphere(eyes.position, sightRange);

#if !UNITY_EDITOR
            // Draw line to player if within sight range and fov
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
#endif

            // Draw suspicious locations as circles
            Gizmos.color = Color.magenta;
            if (suspiciousLocations != null)
            {
                foreach (var loc in suspiciousLocations)
                {
                    Gizmos.DrawWireSphere(loc.Center, loc.Radius);
                }
            }
        }
    }
}