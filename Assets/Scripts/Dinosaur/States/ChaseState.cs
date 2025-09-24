using System;
using System.Numerics;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Scripts.Dinosaur.Abstracts;
using PlasticPipe.PlasticProtocol.Messages;

namespace Assets.Scripts.Dinosaur.States
{
    public class ChaseState : State
    {
        float MaxRoamRadius = 100.0f;

        public ChaseState(DinoContext context) : base(context)
        {
            dinoMovement.Speed = 30f;
            dinoMovement.Acceleration = 20f;
            dinoMovement.StoppingDistance = 0f;
            dinoMovement.DesiredSlowDownDistance = 0f;
            dinoMovement.AutoBraking = true;
        }

        protected override void RunLogic()
        {
            dinoMovement.MoveTo(ctx.Player.position, true);
            ctx.LookTarget.SetTargetLocation(ctx.Player.position);
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