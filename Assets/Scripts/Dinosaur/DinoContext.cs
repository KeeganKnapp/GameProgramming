using UnityEngine;
using UnityEngine.AI;
namespace Assets.Scripts.Dinosaur
{
    public class DinoContext
    {
        public Transform Self { get; set; }
        public DinoSensors DinoSensors { get; set; }

        public DinoMovement DinoMovement { get; set; }

        public Animator Animator { get; set; }

        public Transform Player { get; set; }

        public Terrain Terrain { get; set; }
    }

}