using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace masterFeature
{
    public class GrapplerTether : MonoBehaviour
    {
        GrapplerHook hook;
        public float debugFloat;
        Grappler grappler;
        SpriteRenderer spriteRenderer;
        public float tetherThickness;
        private float tetherLength = 0;

        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            grappler = GetComponentInParent<Grappler>();
            hook = grappler.GetComponentInChildren<GrapplerHook>();
        }

        private void Update()
        {
            //Debug.Log(Mathf.Rad2Deg * Mathf.Acos( Vector3.Dot(Vector3.Normalize(grappler.anchor - grappler.target.transform.position), Vector3.left)));
            //this.gameObject.transform.eulerAngles = new Vector3(0, 0, debugFloat);
            //this.gameObject.transform.localScale = new Vector3(tetherLength, tetherThickness, 0);
            Debug.DrawLine(grappler.anchor, hook.transform.position, new Color(160, 128, 64));
        }
    }
}