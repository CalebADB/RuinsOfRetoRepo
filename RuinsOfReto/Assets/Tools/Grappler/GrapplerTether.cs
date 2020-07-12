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
            grappler = GetComponentInParent<Grappler>();
        }

        public void updateGrappleTether()
        {
            lineRenderer.SetPosition(0, grappler._base.anchor);
            lineRenderer.SetPosition(1, grappler.hook.transform.position);
        }
    }
}