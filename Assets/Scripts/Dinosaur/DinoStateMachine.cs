using System.Threading.Tasks;
using UnityEngine;
using Assets.Scripts.Dinosaur.Abstracts;
using Assets.Scripts.Dinosaur.States;
namespace Assets.Scripts.Dinosaur
{
    public class DinoStateMachine
    {
        public State currentState;
        public DinoStateMachine(DinoContext ctx)
        {
            currentState = new IdleState(ctx);
        }


        public void runState()
        {
            Debug.Log($"Running current state: {currentState.GetType()}");
            State nextState = currentState?.RunCurrentState();
            if (nextState != null)
            {
                SwitchToNextState(nextState);
            }
        }


        private void SwitchToNextState(State nextState)
        {
            currentState = nextState;
        }

    }
}