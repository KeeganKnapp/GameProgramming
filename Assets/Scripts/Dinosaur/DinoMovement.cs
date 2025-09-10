using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using GLTFast;
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


        public async Task MoveTo(Vector3 pos)
        {
            while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
            {
                await Task.Yield();
            }
            agent.SetDestination(pos);

        }

        public void SetSpeed(float Speed)
        {
            agent.speed = Speed;
        }


    }
}