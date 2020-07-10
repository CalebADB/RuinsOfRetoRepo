using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace masterFeature
{
    /// <summary>
    /// This is the players in game cursor. Both paused and in play. It is a little broken in terms of edge confining.
    /// </summary>
    public class Player_Cursor : MonoBehaviour
    {
        private Player_Controller player;
        private CameraGrip cameraGrip;
        public float sensitivity;

        private void Start()
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            if (players.Length == 1) { player = players[0].GetComponentInChildren<Player_Controller>(); }
            else { Debug.Log("More then one object with player tag"); };

            GameObject[] cameraGrips = GameObject.FindGameObjectsWithTag("MainCamera");
            if (cameraGrips.Length == 1) { cameraGrip = cameraGrips[0].GetComponentInChildren<CameraGrip>(); }
            else { Debug.Log("More then one object with cameraGrips tag"); };
        }

        private void Update()
        {
            Cursor.visible = false;

            Vector3 posRaw = new Vector3(VirtualInputManager.Instance.cursorX * sensitivity, VirtualInputManager.Instance.cursorY * sensitivity, 0f);
            Vector3 posNew = fitCursorToScreen(posRaw) + player.transform.position;// fitCursorToScreen(posRaw);
            this.transform.position = posNew;

            Debug.DrawLine(posNew, posNew + (Vector3.one*0.01f), Color.magenta, 10f);
        }

        Vector3 fitCursorToScreen(Vector3 pos)
        {
            Camera cameraCur = cameraGrip.getCameraHeld();
            float cameraMaxRadius = cameraCur.GetComponent<CameraScript>().cameraMaxRadius;

            Vector3 cameraFrame;
            cameraFrame.y = cameraCur.orthographicSize * 2;
            cameraFrame.x = cameraCur.aspect * cameraFrame.y;
            //float playerCursorAngle = Vector3.Dot(Vector3.Normalize(this.transform.position - player.transform.position), Vector3.up) ;
            //Debug.Log(Mathf.Pow(Mathf.Cos(2*playerCursorAngle), 2));
            //Try using magnitudes in the corners and a full two radiuses in the edge middles
            cameraFrame.x += cameraMaxRadius * 2;// (1+(Mathf.Pow(Mathf.Cos(playerCursorAngle*2), 2)));
            cameraFrame.y += cameraMaxRadius * 2;// (1+(Mathf.Pow(Mathf.Cos(playerCursorAngle*2), 2)));

            pos.x *= cameraFrame.x;
            pos.y *= cameraFrame.y;

            return pos;
        }
    }
}