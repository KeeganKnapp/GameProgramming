using System;
using System.Numerics;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Scripts.Dinosaur.Abstracts;

namespace Assets.Scripts.Dinosaur.States
{
    public class ChaseState : State
    {
        float MaxRoamRadius = 100.0f;

        public ChaseState(DinoContext context) : base(context)
        {
            ctx.DinoMovement.Speed = 30f;
            ctx.DinoMovement.Acceleration = 20f;
            ctx.DinoMovement.StoppingDistance = 0f;
            ctx.DinoMovement.DesiredSlowDownDistance = 0f;
            ctx.DinoMovement.AutoBraking = true;
        }

        protected override void RunLogic()
        {
            ctx.DinoMovement.MoveTo(ctx.Player.position, true);
            ctx.HeadRig.SetTargetLocation(ctx.Player.position);
            //returns false always, switch to true depending on what conditions are
            //met to switch states
            shouldChange = false;
        }

        protected override State ReturnNextState()
        {
            //returns this as a place holder
            //add more states up top to swap to based on logic in this function
            return this;
        }
    }
}