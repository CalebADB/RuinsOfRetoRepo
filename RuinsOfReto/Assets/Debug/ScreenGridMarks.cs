using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace masterFeature
{
    public class ScreenGridMarks : MonoBehaviour
    {
        // Update is called once per frame
        void Update()
        {
            Debug.DrawLine(transform.position, transform.position + (Vector3.up / 4), Color.magenta);
        }
    }
}
