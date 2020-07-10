using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace masterFeature
{
    /// <summary>
    /// The PhysicsEngine is the program and container that regulates all universal physics effects
    /// </summary>
    public class PhysicsEngine : MonoBehaviour
    {
        // Time Dialation changes the speed at which the game updates.
        public float timeDialation;
        public Gravity gravity;

        private void Start()
        {
            gravity = this.GetComponent<Gravity>();
        }

        private void Update()
        {
            Time.timeScale = timeDialation;
        }
    }
}