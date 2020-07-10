using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace masterFeature
{
    public class Player_Controller : Controller
    {
        Grappler grappler; 
        private void Start()
        {
            start();
            grappler = GetComponentInChildren<Grappler>();
            grappler.target = GameObject.FindGameObjectWithTag("Cursor");
        }
    }
}

