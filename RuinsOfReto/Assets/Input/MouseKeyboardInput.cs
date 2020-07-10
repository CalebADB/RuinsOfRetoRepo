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
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                VirtualInputManager.Instance.exit = true;
            }
            else
            {
                VirtualInputManager.Instance.exit = false;
            }

            Vector2 cursorNormalized = cameraGrip.getCameraHeld().ScreenToViewportPoint(Input.mousePosition);
            VirtualInputManager.Instance.cursorX = cursorNormalized.x - 0.5f;
            VirtualInputManager.Instance.cursorY = cursorNormalized.y - 0.5f;

            if (Input.GetMouseButton(0))
            {
                Cursor.lockState = CursorLockMode.Confined;
                VirtualInputManager.Instance.button1 = true;
            }
            else
            {
                VirtualInputManager.Instance.button1 = false;
            }
            if (Input.GetMouseButton(1))
            {
                VirtualInputManager.Instance.button2 = true;
            }
            else
            {
                VirtualInputManager.Instance.button2 = false;
            }

            if (Input.GetKey(KeyCode.A))
            {
                VirtualInputManager.Instance.left = true;
            }
            else
            {
                VirtualInputManager.Instance.left = false;
            }
            if (Input.GetKey(KeyCode.D))
            {
                VirtualInputManager.Instance.right = true;
            }
            else
            {
                VirtualInputManager.Instance.right = false;
            }
            if (Input.GetKey(KeyCode.W))
            {
                VirtualInputManager.Instance.up = true;
            }
            else
            {
                VirtualInputManager.Instance.up = false;
            }
            if (Input.GetKey(KeyCode.S))
            {
                VirtualInputManager.Instance.down = true;
            }
            else
            {
                VirtualInputManager.Instance.down = false;
            }
        }
    }
}
