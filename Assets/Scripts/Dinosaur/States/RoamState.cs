using System;
using System.Numerics;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Scripts.Dinosaur.Abstracts;
using UnityEditor.Experimental.GraphView;

namespace Assets.Scripts.Dinosaur.States
{
    public class RoamState : State
    {
        float MaxRoamRadius = 100.0f;
        bool pathSet = false;

        bool shouldBeSuspicious;

        public RoamState(DinoContext context) : base(context)
        {
            dinoMovement.Speed = 10f;
            dinoMovement.Acceleration = 10f;
            dinoMovement.StoppingDistance = 10f;
        }

        protected override void RunLogic()
        {
            Debug.Log($"Remaining distance {dinoMovement.RemainingDistance}");

            //sees player?
            if (dinoSensors.CanSeePlayer)
            {
                shouldBeSuspicious = true;
                shouldChange = true;
            }
            //else set path to move to
            else if (dinoMovement.RemainingDistance <= ctx.DinoMovement.StoppingDistance)
            {
                if (!pathSet)
                {
                    var randomPosition = Helper.RandomLocation(ctx.Self, ctx.Terrain, radius:100f, minRadius:20f, maxAngle:120);
                    Debug.Log($"[Roam]: Moving to {randomPosition.x}, {randomPosition.y}, {randomPosition.z}");
                    pathSet = dinoMovement.MoveTo(randomPosition);
                    ctx.LookTarget.SetTargetLocation(randomPosition);
                    shouldChange = false;
                }
                else
                {
                    shouldChange = true;
                }
            }
        }

        

        protected override State ReturnNextState()
        {
            //returns this as a place holder
            //add more states up top to swap to based on logic in this function
            Dispose();
            if (shouldBeSuspicious)
            {
                ctx.Animator.SetTrigger("Roar");
                return new ChaseState(ctx);
            }
            else
                return new IdleState(ctx);
        }
    }
}