using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace masterFeature
{
    /// <summary>
    /// Gravity is the program and container that regulates all gravitational effects
    /// </summary>
    public class Gravity : MonoBehaviour
    {
        public Vector2 gravityStrength;

        private Vector2 gravity;
        /// <summary>
        /// calculateGravity returns an gravitational 2D acceleration value (multiply by time to get velocity)
        /// </summary>
        public Vector2 calculateGravity(Vector3 location)
        {
            gravity.x = gravityStrength.x;
            gravity.y = -gravityStrength.y;
            return gravity;
        }
    }
}
