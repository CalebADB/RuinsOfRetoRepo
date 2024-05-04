using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldParalaxDriver : MonoBehaviour
{
    [SerializeField] BoxCollider2D playerCollider;
    [SerializeField] float paralaxFactorX;
    [SerializeField] float paralaxFactorY;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 newPosition = transform.position;
        newPosition.x = playerCollider.transform.position.x * paralaxFactorX;
        newPosition.y = playerCollider.transform.position.y * paralaxFactorY;
        transform.position = newPosition;
    }
}
