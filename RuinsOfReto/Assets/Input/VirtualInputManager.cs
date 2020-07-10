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
        public bool exit;

        public float cursorX;
        public float cursorY;
        public bool button1;
        public bool button2;

        public bool left;
        public bool right;
        public bool up;
        public bool down;
    }
}

