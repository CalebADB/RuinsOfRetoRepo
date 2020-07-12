using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace masterFeature
{
    public class Player_Controller : Controller
    {
        /*
        public override int health { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public override bool canRespawn { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public override Vector3 spawn { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public override float deathTimeLength { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        */
        public Vector3 playerSpawn;

        Grappler grappler;
        ProjectileLauncher projectileLauncher;

        private void Start()
        {            
            start();

            health = 4;
            deathTimeLength = 2.0f;
            canRespawn = true;
            spawn = playerSpawn;

            grappler = GetComponentInChildren<Grappler>();
            grappler.target = GameObject.FindGameObjectWithTag("Cursor");
        }
    }
}

