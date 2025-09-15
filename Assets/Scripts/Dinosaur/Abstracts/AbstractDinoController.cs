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
        protected DinoStateMachine dinoStateMachine;
        void Awake()
        {

            Debug.Log("Waking up dino controller!");
            dinoContext = new DinoContext
            {
                Self = transform,
                LookTarget = GameObject.Find("LookTarget"),
                DinoSensors = GetComponent<DinoSensors>(),
                DinoMovement = GetComponent<DinoMovement>(),
                Animator = GetComponent<Animator>(),
                HeadRig = this.GetComponentInChildren<HeadRig>(),
                SelfObject = this.gameObject,
                Player = GameObject.Find("Player").transform,
                Terrain = Terrain.activeTerrain
            };

            dinoStateMachine = new DinoStateMachine(dinoContext);
        }
        protected virtual void Update()
        {
            Animate();
            dinoStateMachine.runState();
        }


        protected void Animate()
        {
            dinoContext.Animator.SetFloat("Acceleration", dinoContext.DinoMovement.Acceleration);
            dinoContext.Animator.SetFloat("MoveSpeed", dinoContext.DinoMovement.VelocityMag);
        }


    }

}