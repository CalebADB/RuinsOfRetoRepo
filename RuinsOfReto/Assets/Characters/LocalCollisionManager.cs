﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace masterFeature
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class LocalCollisionManager : MonoBehaviour
    {
        // Debug:
        public float debugRayMultiplier = 1;

        // Prep:
        public LayerMask collisionMask;
        public new BoxCollider2D collider;
        private RaycastOrigins raycastOrigins;
        private Vector2 rayOrigin;

        public CollisionData collisionData;

        // Raycasting:
        // Skin for avoiding barrier clipping
        const float skinWidth = 0.015f;
        // Values for the resolution of the raycasting (higher resolution is slower but less likely to break)
        public int vertRayCount;
        private float vertRaySpacing;
        public int horzRayCount;
        private float horzRaySpacing;

        private void Start()
        {
            Debug.Log(collider);
            collider = this.GetComponent<BoxCollider2D>();
            calculateRaySpacing();
            Debug.Log(collider);
        }

        private void calculateRaySpacing()
        {
            Bounds bounds = collider.bounds;

            // subtract skin width
            bounds.Expand(skinWidth * -2);

            // make sure theres a minimum of 2 rays
            horzRayCount = Mathf.Clamp(horzRayCount, 2, int.MaxValue);
            vertRayCount = Mathf.Clamp(vertRayCount, 2, int.MaxValue);

            // caleculate ray spacing
            horzRaySpacing = (bounds.size.y + (skinWidth * -2)) / (horzRayCount - 1);
            vertRaySpacing = (bounds.size.x + (skinWidth * -2)) / (vertRayCount - 1);
        }

        /// <summary>
        /// Checks the projected for displacement for objects, and it reduces displacement if a collision is detected
        /// </summary>
        public Vector2 checkDisplacement(Vector2 displacement)
        {
            // RESET Collision Data
            collisionData.horzCollision = false;
            collisionData.rightCollision = false;
            collisionData.leftCollision = false;
            collisionData.vertCollision = false;
            collisionData.topCollision = false;
            collisionData.bottomCollision = false;

            // CHECK to see if the object will collide
            updateRaycastOrigins();
            checkVertCollision(displacement);
            checkHorzCollision(displacement);

            // IF object will collide THEN we calculate and return that distance
            if (collisionData.horzCollision || collisionData.vertCollision)
            {
                return getCollisionDisplacement(displacement);
            }
            // ELSE we return the displacement
            return displacement;
        }

        /// <summary>
        /// Updates origins to parent location. Calculates for a thin skin to prevent clipping
        /// </summary>
        private void updateRaycastOrigins()
        {
            Bounds bounds = collider.bounds;

            // subtracts skin width
            bounds.Expand(skinWidth * -2);

            // Set bounds for collision with skinwidth
            raycastOrigins.bottomLeft.Set(bounds.min.x, bounds.min.y);
            raycastOrigins.bottomRight.Set(bounds.max.x, bounds.min.y);
            raycastOrigins.topLeft.Set(bounds.min.x, bounds.max.y);
            raycastOrigins.topRight.Set(bounds.max.x, bounds.max.y);
        }

        /// <summary>
        /// Checks for Vertical collisions in the direction of travel.
        /// </summary>
        private void checkVertCollision(Vector2 displacement)
        {
            // SETS parameters for raycasting
            Vector2 direction = Vector2.up * Mathf.Sign(displacement.y);
            float rayLength = (displacement.y * -1) + (skinWidth);

            // CHECKS each raycast
            for (int i = 0; i < vertRayCount; i++)
            {
                rayOrigin = (direction.y == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
                rayOrigin.x += (vertRaySpacing * i) + skinWidth;
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, direction, rayLength, collisionMask);

                // IF a raycast encounters an object THEN collisionData is updated
                if (hit)
                {
                    collisionData.collidedObject = hit.collider; 
                    collisionData.vertCollision = true;
                    rayLength = hit.distance;
                    if (direction.y > 0)
                    {
                        collisionData.topCollision = true;
                        collisionData.topCollisionPos = new Vector3(hit.point.x, hit.point.y, 0);
                    }
                    else
                    {
                        collisionData.bottomCollision = true;
                        collisionData.bottomCollisionPos = new Vector3(hit.point.x, hit.point.y, 0);
                    }
                }
                Debug.DrawRay(rayOrigin, direction * rayLength * debugRayMultiplier, Color.magenta);
            }
            collisionData.vertCollisionDistance = (rayLength - skinWidth) * direction.y;
        }

        /// <summary>
        /// Checks for horizontal collisions in the direction of travel.
        /// </summary>
        private void checkHorzCollision(Vector2 displacement)
        {
            // SETS parameters for raycasting
            Vector2 direction = Vector2.right * Mathf.Sign(displacement.x);
            float rayLength = Mathf.Abs(displacement.x);
            rayOrigin = (direction.x == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            rayOrigin.y += skinWidth;

            // CHECKS each raycast
            for (int i = 0; i < horzRayCount; i++)
            {
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, direction, rayLength, collisionMask);

                // IF a raycast encounters an object THEN collisionData is updated
                if (hit)
                {
                    collisionData.collidedObject = hit.collider;
                    collisionData.horzCollision = true;
                    rayLength = hit.distance;
                    if (direction.x > 0)
                    {
                        collisionData.rightCollision = true;
                        collisionData.rightCollisionPos = new Vector3(hit.point.x, hit.point.y, 0);
                    }
                    else
                    {
                        collisionData.leftCollision = true;
                        collisionData.rightCollisionPos = new Vector3(hit.point.x, hit.point.y, 0);
                    }
                }

                Debug.DrawRay(rayOrigin, direction * rayLength * debugRayMultiplier, Color.magenta);
                rayOrigin.y += horzRaySpacing;
            }
            collisionData.horzCollisionDistance = (rayLength - skinWidth) * direction.x;
        }

        /// <summary>
        /// Calculates new displacement using collisionData
        /// </summary>
        public Vector2 getCollisionDisplacement(Vector2 displacement)
        {
            Debug.Log(displacement);
            if (collisionData.horzCollision)
            {
                displacement.x = collisionData.horzCollisionDistance;
            }
            if (collisionData.vertCollision)
            {
                displacement.y = collisionData.vertCollisionDistance;
            }
            if (Mathf.Abs(displacement.y) < 0.01)
            {
                displacement.y = 0;
            }
            if (Mathf.Abs(displacement.x) < 0.01)
            {
                displacement.x = 0;
            }
            return displacement;
        }

        /// <summary>
        /// Any information about the last displacement check is stored in this struct
        /// </summary>
        public struct CollisionData
        {
            // Horizontal info
            public bool horzCollision;
            public bool rightCollision;
            public bool leftCollision;
            public float horzCollisionDistance;
            public Vector3 rightCollisionPos;
            public Vector3 leftCollisionPos;

            // Vertical info
            public bool vertCollision;
            public bool topCollision;
            public bool bottomCollision;
            public float vertCollisionDistance;
            public Vector3 topCollisionPos;
            public Vector3 bottomCollisionPos;

            public Collider2D collidedObject;
        }

        private struct RaycastOrigins
        {
            public Vector2 topLeft, topRight, bottomLeft, bottomRight;
        }
    }
}

