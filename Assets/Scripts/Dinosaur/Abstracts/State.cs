using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Dinosaur {
    public abstract class State
    {

        public State RunCurrentState(DinoContext ctx)
        {
            try
            {
                if (ctx == null)
                {
                    Debug.LogError("Current state context is null, cannot run logic");
                    return this;
                }

                bool shouldChange = RunLogic(ctx);

                if (shouldChange)
                {
                    Debug.Log("[State] returning next state");
                    return ReturnNextState();
                }
                else
                {
                    Debug.Log("[State] staying on this state");
                    return null;
                }
            }
            catch (UnityException ex)
            {
                Debug.LogError($"[State] Exception caught: {ex}");
                return null;
            }
            catch (SystemException ex)
            {
                Debug.LogError($"[State] Exception caught: {ex}");
                return null;
            }
        }

        protected abstract bool RunLogic(DinoContext ctx);

        protected abstract State ReturnNextState();
}


}