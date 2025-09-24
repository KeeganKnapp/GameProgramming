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
        void OnEnable()
        {
            agent = GetComponent<NavMeshAgent>();
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
        public bool MoveTo(Vector3 pos, bool spammable = false)
        {
            Debug.Log($"[DinoMovement] moving to {pos.x} {pos.y} {pos.z}");

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




    }
}
