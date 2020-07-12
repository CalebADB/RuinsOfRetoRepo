using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace masterFeature
{
    public class FireArmBase : MonoBehaviour
    {
        private ProjectileLauncher projectileLauncher;
        private Rigidbody2D rb;
        Vector3 dir;
        public float angle;
        public Vector3 anchor;

        public float baseLength;
        public Vector3 axisOfRotation;

        void Start()
        {
            projectileLauncher = GetComponentInParent<ProjectileLauncher>();
            rb = GetComponentInParent<Rigidbody2D>();
        }

        // Update is called once per frame
        public void updateFireArmBase()
        {
            transform.localPosition = axisOfRotation;
            dir = projectileLauncher.target.transform.position - transform.position;
            angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg + 270f;
            rb.rotation = -angle;

            anchor = transform.position + (dir.normalized * baseLength * transform.localScale.x) + (Vector3.up * 0.11f);
        }
    }
}