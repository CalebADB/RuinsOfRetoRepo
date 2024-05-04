using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace masterFeature
{
    /// <summary>
    /// Medium by which either mouse/keyboard or controller is normalized
    /// </summary>
    public class VirtualInputManager : Singleton<VirtualInputManager>
    {
        public bool exit = false;

        public float cursorX = 0.0f;
        public float cursorY = 0.0f;
        public bool button1 = false;
        public bool button2 = false;

        public bool holdBack = false;
        public bool left = false;
        public bool right = false;
        public bool up = false;
        public bool down = false;
    }
}

