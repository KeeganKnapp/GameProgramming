using System;
using System.Numerics;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Scripts.Dinosaur.Abstracts;

namespace Assets.Scripts.Dinosaur.States
{
    public class SuspiciousState : State
    {
        DinoContext ctx;
        public SuspiciousState(DinoContext context) : base(context)
        {
        }

        protected override void RunLogic()
        {
        }
        protected override State ReturnNextState()
        {
            return this;
        }


    }
}