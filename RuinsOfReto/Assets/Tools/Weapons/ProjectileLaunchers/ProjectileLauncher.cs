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
        public Projectile.ProjectileType projectileType;
        public Sprite reloading;
        public Sprite loaded;
        public Sprite Firing;
        [Range(0.01f, 7f)]
        public float projectileLaunchSpeed;
        [Range(0.01f, 7f)]
        public float weaponLoadTime;
        public bool weaponFired;
        private float weaponLoadTimeCur;
        private bool weaponLoaded;
        public float recoil;
        private float holdShot = 0.15f;

        private void Start()
        {
            weaponLoaded = true;
            target = GameObject.FindGameObjectWithTag("Cursor");
            controller = GetComponentInParent<Controller>();
        }

        private void OnEnable()
        {
            target = GameObject.FindGameObjectWithTag("Cursor");
            controller = GetComponentInParent<Controller>();
            weaponLoaded = true;
        }
        private void OnDisable()
        {
            weaponLoaded = false;
        }

        public void updateProjectileLauncher()
        {
            weaponFired = false;
            if (controller.useWeapon && weaponLoaded)
            {
                GameObject projectileObject = Instantiate(projectilePrefab);

                projectileObject.transform.TryGetComponent<Projectile>(out Projectile projectile);
                projectile.setProjectileType(projectileType);
                projectile.transform.position = _base.anchor;
                projectile.velocity = 10 * projectileLaunchSpeed * Vector3.Normalize(target.transform.position - _base.anchor);
                weaponFired = true;
                recoil = 3 * Mathf.Pow(projectileLaunchSpeed,1.5f);
                weaponLoaded = false;
                weaponLoadTimeCur = 0f;
            }
            if (weaponLoadTimeCur < weaponLoadTime)
            {
                weaponLoadTimeCur += Time.fixedDeltaTime;
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