using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace masterFeature
{
    /// <summary>
    /// Script for reading Input from a keyboard and mouse setup.
    /// </summary>
    public class MouseKeyboardInput : MonoBehaviour
    {
        private CameraGrip cameraGrip;

        private void Start()
        {
            GameObject[] cameraGrips = GameObject.FindGameObjectsWithTag("MainCamera");
            if (cameraGrips.Length == 1) { cameraGrip = cameraGrips[0].GetComponentInChildren<CameraGrip>(); }
            else { Debug.Log("More then one object with cameraGrips tag"); };
        }
        private void FixedUpdate()
        {
            VirtualInputManager.Instance.exit = Input.GetKeyDown(KeyCode.Escape);

            Vector2 cursorNormalized = cameraGrip.getCameraHeld().ScreenToViewportPoint(Input.mousePosition);
            VirtualInputManager.Instance.cursorX = cursorNormalized.x - 0.5f;
            VirtualInputManager.Instance.cursorY = cursorNormalized.y - 0.5f;

            if (Input.GetMouseButton(0))
            {
                Cursor.lockState = CursorLockMode.Confined;
                VirtualInputManager.Instance.button1 = true;
            }
            else { VirtualInputManager.Instance.button1 = false; };
            VirtualInputManager.Instance.button2 = Input.GetMouseButtonDown(1);

            VirtualInputManager.Instance.holdBack = Input.GetKey(KeyCode.LeftShift);
            VirtualInputManager.Instance.left = Input.GetKey(KeyCode.A);
            VirtualInputManager.Instance.right = Input.GetKey(KeyCode.D);
            VirtualInputManager.Instance.up = Input.GetKey(KeyCode.W);
            VirtualInputManager.Instance.down = Input.GetKey(KeyCode.S);
        }
    }
}
