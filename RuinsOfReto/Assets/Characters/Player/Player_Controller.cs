using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

namespace masterFeature
{
    public class Player_Controller : Controller
    {
        /*
        public override int health { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public override bool canRespawn { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public override Vector3 spawn { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public override float deathTimeLength { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        */
        public Vector3 playerSpawn;

        Grappler grappler;
        ProjectileLauncher projectileLauncher;

        private void Start()
        {
            start();

            health = 4;
            deathTimeLength = 2.0f;
            canRespawn = true;
            spawn = playerSpawn;

            grappler = GetComponentInChildren<Grappler>();
            grappler.target = GameObject.FindGameObjectWithTag("Cursor");

            localPhysicsEngine.JumpStart_Event += JumpStart;
            localPhysicsEngine.HitTop_Event += HitTop;
            localPhysicsEngine.HitBottom_Event += HitBottom;
            localPhysicsEngine.HitRight_Event += HitRight;
            localPhysicsEngine.HitLeft_Event += HitLeft;
        }

        private void Update()
        {
            update();
            getHitByEnemy();
            Debug.Log(health);
        }

        public void SwitchToWeapon(WeaponType weaponType)
        {
            switch (weaponType)
            {
                case WeaponType.gerenade:
                    transform.GetChild(1).gameObject.SetActive(true);
                    transform.GetChild(2).gameObject.SetActive(false);
                    transform.GetChild(3).gameObject.SetActive(false);
                    localPhysicsEngine.projectileLauncher = transform.GetChild(1).GetComponent<ProjectileLauncher>();
                    break;
                case WeaponType.missleLuncher:
                    transform.GetChild(1).gameObject.SetActive(false);
                    transform.GetChild(2).gameObject.SetActive(true);
                    transform.GetChild(3).gameObject.SetActive(false);
                    localPhysicsEngine.projectileLauncher = transform.GetChild(2).GetComponent<ProjectileLauncher>();
                    break;
                case WeaponType.plasma:
                    transform.GetChild(1).gameObject.SetActive(false);
                    transform.GetChild(2).gameObject.SetActive(false);
                    transform.GetChild(3).gameObject.SetActive(true);
                    localPhysicsEngine.projectileLauncher = transform.GetChild(3).GetComponent<ProjectileLauncher>();
                    break;
                default:
                    transform.GetChild(1).gameObject.SetActive(true);
                    transform.GetChild(2).gameObject.SetActive(false);
                    transform.GetChild(3).gameObject.SetActive(false);
                    localPhysicsEngine.projectileLauncher = transform.GetChild(1).GetComponent<ProjectileLauncher>();
                    break;
            }
        }

        private void JumpStart(Vector3 jumpPoint)
        {
            //Debug.Log("player jump");
        }

        private void HitTop(Vector3 hitPoint)
        {
            VFXManager.Instance.StartHitTopVFX(hitPoint);
        }

        private void HitBottom(Vector3 hitPoint, bool isMoving)
        {
            VFXManager.Instance.StartHitBottomVFX(hitPoint, isMoving);
        }

        private void HitRight(Vector3 hitPoint)
        {
            VFXManager.Instance.StartHitRightVFX(hitPoint);
        }

        private void HitLeft(Vector3 hitPoint)
        {
            VFXManager.Instance.StartHitLeftVFX(hitPoint);
        }

        void getHitByEnemy()
        {
            if (localPhysicsEngine.localCollisionManager.collisionData.horzCollision || localPhysicsEngine.localCollisionManager.collisionData.horzCollision)
            {
                if (localPhysicsEngine.localCollisionManager.collisionData.collidedObject.CompareTag("Enemy"))
                {
                    takeDamage();
                }
            }
        }
    }
}

