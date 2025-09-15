using System;
using System.Numerics;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Scripts.Dinosaur.Abstracts;

namespace Assets.Scripts.Dinosaur.States
{
    public class RoamState : State
    {
        float MaxRoamRadius = 100.0f;
        bool pathSet = false;

        bool shouldBeSuspicious;

        public RoamState(DinoContext context) : base(context)
        {
            ctx.DinoMovement.Speed = 10f;
            ctx.DinoMovement.Acceleration = 10f;
            ctx.DinoMovement.StoppingDistance = 10f;
        }

        protected override void RunLogic()
        {
            Debug.Log($"Remaining distance {ctx.DinoMovement.RemainingDistance}");

            //sees player?
            if (ctx.DinoSensors.SeesPlayer)
            {
                shouldBeSuspicious = true;
                shouldChange = true;
            }
            //else set path to move to
            else if (ctx.DinoMovement.RemainingDistance <= ctx.DinoMovement.StoppingDistance)
            {
                if (!pathSet)
                {
                    var randomPosition = Helper.RandomLocation(ctx.Self, ctx.Terrain, 100f, 20f);
                    Debug.Log($"[Roam]: Moving to {randomPosition.x}, {randomPosition.y}, {randomPosition.z}");
                    pathSet = ctx.DinoMovement.MoveTo(randomPosition);
                    ctx.HeadRig.SetTargetLocation(randomPosition);
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
                return new ChaseState(ctx);
            else
                return new IdleState(ctx);
        }
    }
}