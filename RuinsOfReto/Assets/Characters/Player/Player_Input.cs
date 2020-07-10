using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace masterFeature
{
    public class Player_Input : MonoBehaviour
    {
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
                        player.pause = true;
                        cameraGrip.cameraHeld = CameraGrip.CameraType.PauseMenu;
                        gameState = GameStates.Pause;
                    }
                    if (VirtualInputManager.Instance.button1)
                    {
                        player.useHook = true;
                    }
                    else
                    {
                        player.useHook = false;
                    }
                    if (VirtualInputManager.Instance.right)
                    {
                        player.moveRight = true;
                    }
                    else
                    {
                        player.moveRight = false;
                    }
                    if (VirtualInputManager.Instance.left)
                    {
                        player.moveLeft = true;
                    }
                    else
                    {
                        player.moveLeft = false;
                    }
                    if (VirtualInputManager.Instance.up)
                    {
                        player.rise = true;
                    }
                    else
                    {
                        player.rise = false;
                    }
                    if (VirtualInputManager.Instance.down)
                    {
                        player.drop = true;
                    }
                    else
                    {
                        player.drop = false;
                    }
                    break;
                case GameStates.Pause:
                    if (VirtualInputManager.Instance.exit)
                    {
                        player.pause = false;
                        cameraGrip.cameraHeld = CameraGrip.CameraType.Play;
                        gameState = GameStates.Play;
                    }
                    break;
            }
        }
    }
}
