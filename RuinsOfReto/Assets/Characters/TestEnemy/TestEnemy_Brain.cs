using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace masterFeature
{
    public class TestEnemy_Brain : MonoBehaviour
    {
        private TestEnemy testEnemy;
        public bool move;
        public bool slow;
        void Start()
        {
            testEnemy = GetComponent<TestEnemy>();
        }

        void Update()
        {
            testEnemy.slow = slow;
            if(move)
            {
                if (testEnemy.moveRight)
                {
                    if (testEnemy.localPhysicsEngine.localCollisionManager.collisionData.horzCollision)
                    {
                        testEnemy.moveRight = false;
                        testEnemy.moveLeft = true;
                    }
                }
                else
                {
                    testEnemy.moveLeft = true;
                    if (testEnemy.localPhysicsEngine.localCollisionManager.collisionData.horzCollision)
                    {
                        testEnemy.moveRight = true;
                        testEnemy.moveLeft = false;
                    }
                }
            }
            else
            {
                testEnemy.moveRight = false;
                testEnemy.moveLeft = false;
            }
        }
    }
}