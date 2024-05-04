using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace masterFeature
{
    public class Player_Input : MonoBehaviour
    {
        [SerializeField] public List<Controller> controllers;
        [SerializeField] private Player_Controller player;
        [SerializeField] private CameraGrip cameraGrip;

        public enum GameStates
        {
            Play,
            Pause
        }
        public GameStates gameState;
        
        private void Start()
        {

        }

        private void FixedUpdate()
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
