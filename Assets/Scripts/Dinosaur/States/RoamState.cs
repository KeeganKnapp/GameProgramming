using System;
using System.Numerics;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Dinosaur
{
    public class RoamState : State
    {
        float MaxRoamRadius = 100.0f;
        protected override bool RunLogic(DinoContext ctx)
        {

                UnityEngine.Vector4 randomOffset = UnityEngine.Random.insideUnitCircle * 100;
                UnityEngine.Vector3 randomPosition =
                    new UnityEngine.Vector3(ctx.Self.transform.position.x, 0.0f, ctx.Self.transform.position.z)
                    + new UnityEngine.Vector3(randomOffset.x, 0.0f, randomOffset.y);

                randomPosition.y = ctx.Terrain.SampleHeight(randomPosition);

                Debug.Log($"[Roam]: Moving to {randomPosition.x}, {randomPosition.y}, {randomPosition.z}");
                ctx.DinoMovement.SetSpeed(5f);
                ctx.DinoMovement.MoveTo(randomPosition);

                //returns false always, switch to true depending on what conditions are
                //met to switch states
                return false;
        }

        protected override State ReturnNextState()
        {
            //returns this as a place holder
            //add more states up top to swap to based on logic in this function
            return this;
        }
    }
}