using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Dinosaur {
public abstract class State
{
    
    public abstract Task<State> runCurrentState(DinoContext ctx);
}


}