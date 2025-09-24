using System;
using System.Threading.Tasks;
using Codice.CM.WorkspaceServer;
using UnityEngine;

namespace Assets.Scripts.Dinosaur.Abstracts {
    public abstract class State : IDisposable
    {
        protected DinoContext ctx;

        protected DinoMovement dinoMovement;
        protected DinoSensors dinoSensors;

        protected bool shouldChange;

        public State(DinoContext context)
        {
            ctx = context;
            dinoMovement = ctx.DinoMovement;
            dinoSensors = ctx.DinoSensors;
        }
        public State RunCurrentState()
        {
            try
            {
                if (ctx == null)
                {
                    Debug.LogError("Current state context is null, cannot run logic");
                    return this;
                }

                RunLogic();

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
        protected abstract void RunLogic();

        protected abstract State ReturnNextState();

        public void Dispose()
        {

        }

    }
}