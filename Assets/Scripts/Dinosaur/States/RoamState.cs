using System.Numerics;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Dinosaur
{
    public class RoamState : State
    {
        [SerializeField] float MaxRoamRadius = 100.0f;
        public override async Task<State> runCurrentState(DinoContext ctx)
        {
            ctx.Animator.Play("Walk");

            UnityEngine.Vector3 randomOffset = Random.insideUnitCircle * 100;
            UnityEngine.Vector3 randomPosition =
                new UnityEngine.Vector3(ctx.Self.transform.position.x, 0.0f, ctx.Self.transform.position.z)
                + new UnityEngine.Vector3(randomOffset.x, 0.0f, randomOffset.y);

            randomPosition.y = ctx.Terrain.SampleHeight(randomPosition);

            ctx.DinoMovement.SetSpeed(5f);
            await ctx.DinoMovement.MoveTo(randomPosition);
            return this;
        }
    }
}