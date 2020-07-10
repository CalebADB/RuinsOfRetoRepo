using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace masterFeature
{
    public class CameraScript : MonoBehaviour
    {
        public CameraShaker cameraShaker;

        public bool UPDATE_VARIABLES;
        public bool steadyCamera;
        [Range(0.01f, 10f)]
        public float cameraMaxRadius;
        [Range(0.01f, 4f)]
        public float cameraCursorPullFactor;
        [Range(0.0001f, 0.5f)]
        public float cameraFollowFactor;
        public float radialAndSeverityRatio;
        public float flatteningValue;

        // Start is called before the first frame update
        void Start()
        {
            cameraShaker = this.GetComponent<CameraShaker>();
        }

        // Update is called once per frame
        void Update()
        {
            if (!steadyCamera) { cameraShaker.shake(); };
        }

        public void updateCameraVariables()
        {
            radialAndSeverityRatio = cameraMaxRadius / cameraCursorPullFactor;
            flatteningValue = -(radialAndSeverityRatio / (Mathf.Log(2)));
        }
    }
}