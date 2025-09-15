using System;
using System.Diagnostics;
using System.Numerics;
using System.Security.Cryptography;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Scripts.Dinosaur.Abstracts;

namespace Assets.Scripts.Dinosaur.States
{
    public enum IdleAction
    {
        LookLeft,
        LookRight,
        Roam
    }
    public class IdleState : State
    {
        private double startTime;

        private double randomActionTimeSeconds;

        public IdleState(DinoContext context) : base(context)
        {
            startTime = DateTime.Now.TimeOfDay.TotalSeconds;
            randomActionTimeSeconds = UnityEngine.Random.Range(3, 10);

        }
        protected override void RunLogic()
        {
            double nowTime = DateTime.Now.TimeOfDay.TotalSeconds;
            if (nowTime - startTime > randomActionTimeSeconds)
            {
                RunRandomAction();
            }
        }

        private void RunRandomAction()
        {
            //IdleAction idleAction = GetRandomAction();
            IdleAction idleAction = GetRandomAction();
            switch (idleAction)
            {
                case IdleAction.LookLeft:
                    ctx.HeadRig.SetTargetLocation(Helper.RandomLocation(ctx.Self, ctx.Terrain));
                    break;
                case IdleAction.LookRight:
                    ctx.HeadRig.SetTargetLocation(Helper.RandomLocation(ctx.Self, ctx.Terrain));
                    break;
                case IdleAction.Roam:
                    shouldChange = true;
                    break;
            }
            resetActionTime();
        }
        private void resetActionTime()
        {
            startTime = DateTime.Now.TimeOfDay.TotalSeconds;
        }
        public static IdleAction GetRandomAction()
        {
            System.Random rng = new System.Random();
            Array values = Enum.GetValues(typeof(IdleAction));
            int index = rng.Next(values.Length);
            return (IdleAction)values.GetValue(index)!;
        }
        protected override State ReturnNextState()
        {
            Dispose();
            if (shouldChange)
            {
                return new RoamState(ctx);
            }
            else
            {
                return this;
            }
        }
    }
}