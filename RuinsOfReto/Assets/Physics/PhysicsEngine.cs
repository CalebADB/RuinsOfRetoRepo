using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace masterFeature
{
    /// <summary>
    /// The PhysicsEngine is the program and container that regulates all universal physics properties
    /// </summary>
    public class PhysicsEngine : Singleton<PhysicsEngine>
    {
        // Time Dialation changes the speed at which the game updates.
        public float timeDialation;
        public Gravity gravity;

        private void Awake()
        {
            gravity = GetComponent<Gravity>();
        }

        private void Update()
        {
            Time.timeScale = timeDialation;
        }
    }
}