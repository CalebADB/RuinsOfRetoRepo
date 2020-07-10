using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace masterFeature
{
    public class CameraShaker : MonoBehaviour
    {
        private float shakeSeed;

        [Range(0f, 20f)]
        public float shakeAngleMax;
        [Range(0f, 1f)]
        public float shakeOffsetMaxX;
        [Range(0f, 1f)]
        public float shakeOffsetMaxY;
        [Range(0f, 20f)]
        public float shakeMaxSpeed;
        [Range(0f, 2.5f)]
        public float traumaDampeningFactor;
        private float traumaDampening;
        public float traumaCur;

        private void Start()
        {
            shakeSeed = Random.Range(0f, 1f);
        }

        // Update is called once per frame
        private void Update()
        {
            traumaDampening = traumaDampeningFactor / 100;
            if (traumaCur > 0f)
            {
                traumaCur -= traumaDampening;
            }
            else
            {
                traumaCur = 0f;
            };
        }

        /// <summary>
        /// A percentage over 100% will act the same as 100%
        /// </summary>
        /// <param name="traumaPercent"></param>
        public void addTrauma(float traumaPercent)
        {
            float traumaNew = traumaPercent / 100f;
            traumaCur += traumaNew;
            if (traumaCur > 1f) { traumaCur = 1f; };
        }

        public void shake()
        {
            if (traumaCur > 0.001f)
            {
                Vector3 shakyOffSet = Vector3.zero;
                shakyOffSet.x  = shakeOffsetMaxX * Mathf.Pow(traumaCur, 2) * (2 * ((Mathf.PerlinNoise(shakeSeed + 0f, Time.time * shakeMaxSpeed) - 0.45f)));
                shakyOffSet.y  = shakeOffsetMaxY * Mathf.Pow(traumaCur, 2) * (2 * ((Mathf.PerlinNoise(shakeSeed + 4f, Time.time * shakeMaxSpeed) - 0.45f)));
                float shakyAngle = shakeAngleMax * Mathf.Pow(traumaCur, 2) * (2 * ((Mathf.PerlinNoise(shakeSeed + 2f, Time.time * shakeMaxSpeed) - 0.45f)));

                this.gameObject.transform.localPosition = shakyOffSet;
                this.gameObject.transform.rotation = Quaternion.AngleAxis(shakyAngle, Vector3.forward);
            }
            else
            {
                this.gameObject.transform.localPosition = Vector3.zero;
                this.gameObject.transform.rotation = Quaternion.Euler(Vector3.zero);
            }
        }
    }
}