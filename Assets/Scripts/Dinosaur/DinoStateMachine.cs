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


        public async Task runState()
        {
            State nextState = await currentState?.runCurrentState(_ctx);
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