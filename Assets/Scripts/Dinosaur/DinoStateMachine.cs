using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Dinosaur
{
    public class DinoStateMachine
    {

        public State currentState = new RoamState();
        private DinoContext _ctx;
        public DinoStateMachine(DinoContext ctx)
        {
            _ctx = ctx;
        }


        public void runState()
        {
            Debug.Log($"Running current state: {currentState.GetType()}");
            State nextState = currentState?.RunCurrentState(_ctx);
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