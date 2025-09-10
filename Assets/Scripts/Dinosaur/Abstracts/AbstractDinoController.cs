using System.Threading.Tasks;
using PlasticPipe.PlasticProtocol.Messages;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Scripting;

namespace Assets.Scripts.Dinosaur
{
    public abstract class AbstractDinoController : MonoBehaviour
    {
        [SerializeField] protected Transform player;
        [SerializeField] protected Terrain terrain;
        protected DinoContext dinoContext;
        protected DinoStateMachine dinoStateMachine;
        void Awake()
        {
            Debug.Log("Waking up dino controller!");
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
        protected virtual void Update()
        {
            Animate();
            if (dinoStateMachine == null)
            {
                Debug.LogError("[DinoController] State machine is null, cannot update");
            }
            else if (dinoContext.DinoMovement == null)
            {
                Debug.LogError("[DinoController] Movement script is null, cannot move");
            }
            else if (dinoContext.Animator == null)
            {
                Debug.LogError("[DinoController] Animator is null, cannot animate");
            }
            dinoStateMachine.runState();
        }

        public void Animate()
        {
            dinoContext.Animator.Play("Walk");
        }
    }

}