using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using PlasticGui;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Dinosaur
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(DinoController))]
    public class DinoMovement : MonoBehaviour
    {
        // Start is called before the first frame update
        private NavMeshAgent agent;

        public float RemainingDistance { get { return agent.remainingDistance; } set { } }
        public float StoppingDistance { get { return agent.stoppingDistance; } set { agent.stoppingDistance = value; } }
        public float Speed { get { return agent.speed; } set { agent.speed = value; } }
        public float Acceleration { get { return agent.acceleration; } set { agent.acceleration = value; } }
        public float VelocityMag { get { return agent.velocity.magnitude; } set { } }
        public bool AutoBraking { get { return agent.autoBraking; } set { agent.autoBraking = value; } }
        public float DesiredSlowDownDistance { get; set; }

        private float _decelleration { get; set; }

        DinoContext ctx;
        void OnEnable()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        void Start()
        {
            ctx = GetComponent<DinoController>().dinoContext;
        }

        void Update()
        {
            SmoothStop();
        }

        public void SmoothStop()
        {
            if (agent.pathPending || !agent.hasPath) return;

            float v = agent.velocity.magnitude;
            float dist = Mathf.Max(agent.remainingDistance, 0.0001f);

            if (dist <= DesiredSlowDownDistance && v > 0.01f)
            {
                // Use live 'dist' so the decay tightens as you approach
                float f = 1f - (v * Time.deltaTime) / dist;
                // keep within a sensible range; avoid collapsing to zero too early
                f = Mathf.Clamp(f, 0.90f, 0.999f);

                agent.velocity *= f;
            }

            agent.autoBraking = true; // let NavMesh do the final ease
        }
        public void LookAtTarget()
        {
            if (agent.hasPath)
            {
                Vector3 lookPos = agent.destination;
                ctx.LookTarget.SetTargetLocation(lookPos);
            }
        }
        public void CancelPath()
        {
            if (agent.hasPath)
            {
                agent.ResetPath();
            }
        }


        public bool MoveTo(Vector3 pos, bool spammable = false)
        {
            Debug.Log($"[DinoMovement] moving to {pos.x} {pos.y} {pos.z}");

            //check that position isnt within stopping distance
            if (Vector3.Distance(transform.position, pos) <= StoppingDistance)
            {
                Debug.Log("[DinoMovement.MoveTo] already within stopping distance of target");
                return false;
            }
            //check for agent
            if (!agent)
            {
                Debug.LogError("[DinoMovement.MoveTo] no NavMeshAgent, cannot move to position");
                return false;
            }

            if ((agent.remainingDistance <= agent.stoppingDistance) || spammable)
            {
                bool set = agent.SetDestination(pos);
                return set;
            }

            if (agent.pathStatus == NavMeshPathStatus.PathInvalid)
            {
                Debug.Log("[DinoMovement.MoveTo] Path invalid");
                return false;
            }

            return false;
        }

        public void MoveToNow(Vector3 pos)
        {
            Debug.Log($"[DinoMovement] moving to {pos.x} {pos.y} {pos.z}");

            //check for agent
            if (!agent)
            {
                Debug.LogError("[DinoMovement.MoveTo] no NavMeshAgent, cannot move to position");
                return;
            }

            agent.SetDestination(pos);

            if (agent.pathStatus == NavMeshPathStatus.PathInvalid)
            {
                Debug.Log("[DinoMovement.MoveTo] Path invalid");
                return;
            }
        }

        private void OnDrawGizmos()
        {
            if (agent != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, agent.destination);
            }

            if (ctx != null && ctx.LookTarget != null && ctx.LookTarget.targetWorld != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(ctx.LookTarget.targetWorld, 0.5f);
            }
        }


    }
}
