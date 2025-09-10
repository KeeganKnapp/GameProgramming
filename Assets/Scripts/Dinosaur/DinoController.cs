using GLTFast.Schema;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Scripting;

namespace Assets.Scripts.Dinosaur
{
    public class DinoController : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private Terrain terrain;
        private DinoContext dinoContext;
        private DinoStateMachine dinoStateMachine;
        void Awake()
        {
            dinoContext = new DinoContext
            {
                Self = transform,
                DinoSensors = GetComponent<DinoSensors>(),
                DinoMovement = GetComponent<DinoMovement>(),
                Animator = GetComponent<Animator>(),
                Player = player,
                Terrain = terrain
            };

            dinoStateMachine = new DinoStateMachine(dinoContext);
        }
        void Update()
        {
            dinoStateMachine.runState();
        }
    }

}