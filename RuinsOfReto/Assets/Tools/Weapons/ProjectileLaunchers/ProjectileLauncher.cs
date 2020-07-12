using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace masterFeature
{
    public class ProjectileLauncher : MonoBehaviour
    {
        public Controller controller;
        public FireArmBase _base;
        public GameObject target;
        public GameObject projectilePrefab;
        public Sprite reloading;
        public Sprite loaded;
        public Sprite Firing;
        public float projectileLaunchSpeed;
        public float weaponLoadTime;
        public bool weaponFired;
        private float weaponLoadTimeCur;
        private bool weaponLoaded;
        public float recoil;
        private float holdShot = 0.2f;

        private void Start()
        {
            weaponLoaded = true;
            target = GameObject.FindGameObjectWithTag("Cursor");
            controller = GetComponentInParent<Controller>();
        }

        public void updateProjectileLauncher()
        {
            recoil = 2 * projectileLaunchSpeed;
            weaponFired = false;
            if (controller.useWeapon && weaponLoaded)
            {
                Debug.Log("FIRE");
                GameObject projectileObject = Instantiate(projectilePrefab);
                projectileObject.transform.parent = GameObject.FindGameObjectWithTag("ProjectileContainer").transform;

                projectileObject.transform.TryGetComponent<Projectile>(out Projectile projectile);
                projectile.transform.position = _base.anchor;
                projectile.velocity = 10 * projectileLaunchSpeed * Vector3.Normalize(target.transform.position - _base.anchor);
                weaponFired = true;
                weaponLoaded = false;
                weaponLoadTimeCur = 0f;
            }
            if (weaponLoadTimeCur < weaponLoadTime)
            {
                weaponLoadTimeCur += Time.deltaTime;
            }
            else
            {
                weaponLoaded = true;
            }

            // animation
            if (controller.useWeapon && weaponLoaded || (holdShot > weaponLoadTimeCur))
            {
                GetComponentInChildren<SpriteRenderer>().sprite = Firing;
            }
            else if (weaponLoaded)
            {
                GetComponentInChildren<SpriteRenderer>().sprite = loaded;
            }
            else
            {
                GetComponentInChildren<SpriteRenderer>().sprite = reloading;
            }
        }
    }
}