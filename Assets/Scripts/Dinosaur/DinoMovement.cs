using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PlasticGui;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Dinosaur
{
    public class DinoMovement : MonoBehaviour
    {
        float Speed = 20f;
        // Start is called before the first frame update
        [SerializeField] NavMeshAgent agent;
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
        }


        public bool MoveTo(Vector3 pos)
        {
            Debug.Log($"[DinoMovement] moving to {pos.x} {pos.y} {pos.z}");

            //check for agent
            if (!agent)
            {
                Debug.LogError("[DinoMovement.MoveTo] no NavMeshAgent, cannot move to position");
                return false;
            }

            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                bool set = agent.SetDestination(pos);
                return true;
            }

            if (agent.pathStatus == NavMeshPathStatus.PathInvalid)
            {
                Debug.Log("[DinoMovement.MoveTo] Path invalid");
                return false;
            }

            return false;
        }

        public void SetSpeed(float Speed)
        {
            agent.speed = Speed;
        }


    }
}