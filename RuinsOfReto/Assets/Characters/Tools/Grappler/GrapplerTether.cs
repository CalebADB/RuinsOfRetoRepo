using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace masterFeature
{
    public class GrapplerTether : MonoBehaviour
    {
        private Grappler grappler;

        LineRenderer lineRenderer;
        public float tetherThickness;

        private void Start()
        {
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.startWidth = tetherThickness;
            lineRenderer.startColor = new Color(160, 128, 64);
            lineRenderer.endColor = new Color(160, 128, 64);
            Material whiteDiffuseMat = new Material(Shader.Find("Unlit/Texture"));
            grappler = GetComponentInParent<Grappler>();
        }

        private void Update()
        {
            lineRenderer.SetPosition(0, grappler._base.anchor);
            lineRenderer.SetPosition(1, grappler.hook.transform.position);
        }
    }
}