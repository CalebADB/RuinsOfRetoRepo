using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace masterFeature
{
    /// <summary>
    /// The camera grip follows the player and to the game types respective camera
    /// </summary>
    public class CameraGrip : MonoBehaviour
    {
        // SETUP
        private Player_Controller player;
        private Player_Cursor cursor;

        public CameraScript camera_Play;
        public CameraScript camera_PauseMenu;

        private Vector2 cameraPos;
        private Vector2 playerPos;
        private Vector2 cursorPos;

        public enum CameraType
        {
            Play,
            PauseMenu
        }
        public CameraType cameraHeld;
        private CameraType cameraHeldLastFrame;

        // Camera Anchor Equation Variables        
        [Range(-20f, 0f)]
        public float cameraGripDistance;
        private float cameraMaxRadius;
        private float cameraCursorPullFactor;
        private float cameraFollowFactor;

        private float radialAndSeverityRatio;
        private float flatteningValue;

        private void Start()
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            if (players.Length == 1) { player = players[0].GetComponentInChildren<Player_Controller>(); }
            else { Debug.Log("More then one object with player tag"); };

            GameObject[] cursors = GameObject.FindGameObjectsWithTag("Cursor");
            if (cursors.Length == 1) { cursor = cursors[0].GetComponentInChildren<Player_Cursor>(); }
            else { Debug.Log("More then one object with cursor tag"); };

            CameraScript[] cameraScripts = GetComponentsInChildren<CameraScript>();
            if (cameraScripts.Length == 2)
            {
                foreach (CameraScript cameraScript in cameraScripts)
                {
                    if (cameraScript.gameObject.name == "Camera_Play")
                    {
                        camera_Play = cameraScript;
                    }
                    else if (cameraScript.gameObject.name == "Camera_PauseMenu")
                    {
                        camera_PauseMenu = cameraScript;
                    }
                    else
                    {
                        Debug.Log("Camera not found");
                    }
                }
            }
            else { Debug.Log("There should only be " + 2 + " cameras"); };

            useCamera(cameraHeld);

            this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, cameraGripDistance);
        }

        // Update is called once per frame
        void Update()
        {
            if((cameraHeld != cameraHeldLastFrame) || (camera_Play.UPDATE_VARIABLES || camera_Play.UPDATE_VARIABLES))
            {
                useCamera(cameraHeld);
            }

            switch (cameraHeld)
            {
                case CameraType.Play:
                    camera_Play.cameraShaker.addTrauma(player.impactStrengthPercent);
                    camera_Play.cameraShaker.shake();
                    break;
                case CameraType.PauseMenu:
                    break;
            }
            cameraHeldLastFrame = cameraHeld;
        }

        private void FixedUpdate()
        {
            followPlayer();
        }

        public Camera getCameraHeld()
        {
            switch (cameraHeld)
            {
                case (CameraType.Play):
                    return camera_Play.GetComponent<Camera>();
                case (CameraType.PauseMenu):
                    return camera_PauseMenu.GetComponent<Camera>();
            }

            Debug.Log("Camera type not selected.");
            return Camera.current;
        }

        void followPlayer()
        {
            cameraPos = this.transform.position;
            playerPos = player.transform.position;
            cursorPos = cursor.transform.position;

            Vector2 playerToCursor = cursor.transform.position - player.transform.position;
            Vector2 unitVectPlayerToCursor = playerToCursor.normalized;
            float distPlayerToCursor = playerToCursor.magnitude;

            // Complicated function. See desmos file for more info: https://www.desmos.com/calculator/v6itjtqy3o
            float distPlayerToAnchor = cameraCursorPullFactor * ((flatteningValue * Mathf.Log(1 + (Mathf.Pow(2, distPlayerToCursor / radialAndSeverityRatio)))) + distPlayerToCursor + radialAndSeverityRatio);
            if (distPlayerToAnchor > (3 * distPlayerToCursor /4))
            {
                distPlayerToAnchor = (3 * distPlayerToCursor /4);
            }
            Vector2 cameraTarget = playerPos + (distPlayerToAnchor * unitVectPlayerToCursor);
            Vector3 newCameraPos = (cameraTarget * cameraFollowFactor) + (cameraPos * (1f - cameraFollowFactor));
            newCameraPos.z = cameraGripDistance;
            this.transform.position = newCameraPos;
        }

        public void useCamera(CameraType cameraHeld)
        {
            Camera[] cameraScripts = GetComponentsInChildren<Camera>();
            switch (cameraHeld)
            {
                case (CameraType.Play):
                    camera_Play.GetComponent<Camera>().enabled = true;
                    camera_PauseMenu.GetComponent<Camera>().enabled = false;

                    cameraMaxRadius = camera_Play.cameraMaxRadius;
                    cameraCursorPullFactor = camera_Play.cameraCursorPullFactor;
                    cameraFollowFactor = camera_Play.cameraFollowFactor;
                    camera_Play.updateCameraVariables();
                    radialAndSeverityRatio = camera_Play.radialAndSeverityRatio;
                    flatteningValue = camera_Play.flatteningValue;


                    break;
                case (CameraType.PauseMenu):
                    camera_Play.GetComponent<Camera>().enabled = false;
                    camera_PauseMenu.GetComponent<Camera>().enabled = true;

                    cameraMaxRadius = camera_PauseMenu.cameraMaxRadius;
                    cameraCursorPullFactor = camera_PauseMenu.cameraCursorPullFactor;
                    cameraFollowFactor = camera_PauseMenu.cameraFollowFactor;
                    camera_PauseMenu.updateCameraVariables();
                    radialAndSeverityRatio = camera_PauseMenu.radialAndSeverityRatio;
                    flatteningValue = camera_PauseMenu.flatteningValue;

                    this.transform.position = player.transform.position;
                    break;
            }
            
        }
    }
}
