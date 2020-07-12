using masterFeature;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace masterFeature
{
    public class WeaponDropManager : MonoBehaviour
    {
        public GameObject weaponIndicatorPrefab;

        public WeaponDropsList levelWeaponDrops;

        public Sprite gerenadeSprite;
        public Sprite missleLuncherSprite;
        public Sprite plasmaSprite;

        public float WEAPON_DROP_COLLECT_RADIUS;

        private List<GameObject> weapnDropsList;

        void Start()
        {
            SpawnAllWeaponDrops();
        }

        public void SpawnAllWeaponDrops()
        {
            for (int i = 0; i < levelWeaponDrops.positions.Count; i++)
            {
                GameObject weaponDropIndicatorGameObject = ObjectPool.Spawn(weaponIndicatorPrefab, levelWeaponDrops.positions[i]);
                switch (levelWeaponDrops.types[i])
                {
                    case WeaponType.gerenade:
                        weaponDropIndicatorGameObject.GetComponent<SpriteRenderer>().sprite = gerenadeSprite;
                        break;
                    case WeaponType.missleLuncher:
                        weaponDropIndicatorGameObject.GetComponent<SpriteRenderer>().sprite = missleLuncherSprite;
                        break;
                    case WeaponType.plasma:
                        weaponDropIndicatorGameObject.GetComponent<SpriteRenderer>().sprite = plasmaSprite;
                        break;
                    default:
                        break;
                }
            }
        }

        public void DespawnAllWeaponDrops()
        {
            for (int i = 0; i < weapnDropsList.Count; i++)
            {
                ObjectPool.Despawn(weapnDropsList[i]);
            }
        }

        public WeaponType GetCollectableWeaponDrop(Vector3 playerPos)
        {
            WeaponType resultWeaponType = WeaponType.NULL;
            for (int i = 0; i < levelWeaponDrops.positions.Count; i++)
            {
                if(Vector3.Distance(playerPos, levelWeaponDrops.positions[i]) < WEAPON_DROP_COLLECT_RADIUS)
                {
                    resultWeaponType = levelWeaponDrops.types[i];
                    ObjectPool.Despawn(weapnDropsList[i]);
                }
            }
            return resultWeaponType;
        }
    }
}
