using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Dinosaur
{
    public class DinoSensors : MonoBehaviour
    {
        [SerializeField] Transform eyes;
        [SerializeField] float sightRange = 25f;
        [SerializeField] float fovDegrees = 110f;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public bool canSeePlayer()
        {
            return false;
        }
    }
}