using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlasticPipe.PlasticProtocol.Messages;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Scripting;
using UnityEngine.Animations.Rigging;
using System.Numerics;

namespace Assets.Scripts.Dinosaur.Abstracts
{
    public abstract class AbstractDinoController : MonoBehaviour
    {
        [SerializeField] protected Transform player;
        [SerializeField] protected Terrain terrain;

        public DinoContext dinoContext;
        private DinoMovement dinoMovement;
        private Animator animator;
        protected DinoStateMachine dinoStateMachine;
        void Awake()
        {

            Debug.Log("Waking up dino controller!");
            dinoContext = new DinoContext
            {
                Self = transform,
                LookTarget = GameObject.Find("LookTarget").GetComponent<LookTarget>(),
                DinoSensors = GetComponent<DinoSensors>(),
                DinoMovement = GetComponent<DinoMovement>(),
                Animator = GetComponent<Animator>(),
                SelfObject = this.gameObject,
                Player = GameObject.Find("Player").transform,
                Terrain = Terrain.activeTerrain
            };

            dinoStateMachine = new DinoStateMachine(dinoContext);

            animator = GetComponent<Animator>();
            dinoMovement = GetComponent<DinoMovement>();
        }
        protected virtual void Update()
        {
            Animate();
            dinoStateMachine.runState();
        }


        protected void Animate()
        {
            animator.SetFloat("Acceleration", dinoMovement.Acceleration);
            animator.SetFloat("MoveSpeed", dinoMovement.VelocityMag);
        }


    }

}