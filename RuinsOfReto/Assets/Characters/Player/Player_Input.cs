using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace masterFeature
{
    public class Player_Input : MonoBehaviour
    {
        private Controller[] controllers;
        private Player_Controller player;
        private CameraGrip cameraGrip;

        public enum GameStates
        {
            Play,
            Pause
        }
        public GameStates gameState;
        
        private void Start()
        {
            controllers = FindObjectOfType<ControllerRegister>().controllers;
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            if (players.Length == 1) { player = players[0].GetComponentInChildren<Player_Controller>(); }
            else { Debug.Log("More then one object with player tag"); };

            GameObject[] cameraGrips = GameObject.FindGameObjectsWithTag("MainCamera");
            if (cameraGrips.Length == 1) { cameraGrip = cameraGrips[0].GetComponentInChildren<CameraGrip>(); }
            else { Debug.Log("More then one object with MainCamera tag"); };
        }

        private void Update()
        {
            
            switch (gameState)
            {
                case GameStates.Play:
                    Cursor.lockState = CursorLockMode.Confined;
                    if (VirtualInputManager.Instance.exit)
                    {
                        foreach (Controller controller in controllers)
                        {
                            controller.pause = true;
                        }
                        cameraGrip.cameraHeld = CameraGrip.CameraType.PauseMenu;
                        gameState = GameStates.Pause;
                    }
                    player.useHook = VirtualInputManager.Instance.button1;
                    player.useWeapon = VirtualInputManager.Instance.button2;

                    player.slow = VirtualInputManager.Instance.holdBack;
                    player.moveRight = VirtualInputManager.Instance.right;
                    player.moveLeft = VirtualInputManager.Instance.left;
                    player.rise = VirtualInputManager.Instance.up;
                    player.drop = VirtualInputManager.Instance.down;
                    break;
                case GameStates.Pause:
                    if (VirtualInputManager.Instance.exit)
                    {
                        foreach (Controller controller in controllers)
                        {
                            controller.pause = false;
                        }
                        cameraGrip.cameraHeld = CameraGrip.CameraType.Play;
                        gameState = GameStates.Play;
                    }
                    break;
            }
        }
    }
}
