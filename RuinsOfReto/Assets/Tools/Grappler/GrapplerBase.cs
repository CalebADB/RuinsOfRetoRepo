using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace masterFeature
{
    public class GrapplerBase : MonoBehaviour
    {
        private Grappler grappler;
        private Rigidbody2D rb;
        Vector3 dir;
        public float angle;
        public Vector3 anchor;

        public float baseLength;
        public Vector3 axisOfRotation;

        void Start()
        {
            grappler = GetComponentInParent<Grappler>();
            rb = GetComponentInParent<Rigidbody2D>();
        }

        // Update is called once per frame
        public void updateGrapplerBase()
        {
            transform.localPosition = axisOfRotation;
            dir = grappler.target.transform.position - transform.position;
            angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg - 90f;
            rb.rotation = -angle;

            anchor = transform.position + (dir.normalized * baseLength * transform.localScale.x);
        }
    }
}