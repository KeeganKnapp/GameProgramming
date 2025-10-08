using System;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Scripts.Dinosaur;
using Assets.Scripts.Dinosaur.Abstracts;
using Palmmedia.ReportGenerator.Core.Parser.Analysis;

namespace Assets.Scripts.Dinosaur.States
{
    public class SuspiciousState : State
    {
        private SuspiciousLocation lastTarget = null;

        public SuspiciousState(DinoContext context) : base(context)
        {
            dinoMovement.Speed = 20f;
        }

        protected override void RunLogic()
        {
            shouldChange = false;

            var target = ctx.SuspiciousLocations
                .OrderByDescending(s => s.Confidence)
                .FirstOrDefault();

            if (target != lastTarget)
            {
                lastTarget = target;
                dinoMovement.CancelPath();
            }

            var targetRandomOffset = Helper.RandomLocationWithinRadiusNoDirection(
                target.Center, 
                ctx.Terrain, 
                target.Radius, 
                0f
            );

            dinoMovement.Speed = Mathf.Lerp(0f, 15f, target.Confidence);
            dinoMovement.Acceleration = Mathf.Lerp(10f, 30f, target.Confidence);
            dinoMovement.MoveTo(targetRandomOffset);
            dinoMovement.LookAtTarget();
        }

        protected override State ReturnNextState()
        {
            return this;
        }
    }
}