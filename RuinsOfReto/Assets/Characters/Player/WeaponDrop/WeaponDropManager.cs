using masterFeature;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace masterFeature
{
    public class WeaponDropManager : MonoBehaviour
    {
        public GameObject weaponIndicatorPrefab;

        public Sprite gerenadeSprite;
        public Sprite missleLuncherSprite;
        public Sprite plasmaSprite;

        public float WEAPON_DROP_COLLECT_RADIUS;

        private List<Vector3> weaponDropPosList;
        private List<WeaponType> weaponDropTypeList;
        private List<GameObject> weapnDropsGOList;
        private Player_Controller player_Controller;

        void Start()
        {
            weapnDropsGOList = new List<GameObject>();
            weaponDropPosList = new List<Vector3>();
            weaponDropTypeList = new List<WeaponType>();
            FindAllWeaponDrops();
            SpawnAllWeaponDrops();
            player_Controller = transform.parent.GetComponentInChildren<Player_Controller>();
        }

        void Update()
        {
            CollectWeaponDrop();
        }

        private void FindAllWeaponDrops()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                weapnDropsGOList.Add(transform.GetChild(i).gameObject);
                weaponDropPosList.Add(transform.GetChild(i).position);
                weaponDropTypeList.Add(transform.GetChild(i).GetComponent<WeaponDropData>().weaponType);
            }
        }

        public void SpawnAllWeaponDrops()
        {
            for (int i = 0; i < weaponDropPosList.Count; i++)
            {
                switch (weaponDropTypeList[i])
                {
                    case WeaponType.gerenade:
                        weapnDropsGOList[i].GetComponent<SpriteRenderer>().sprite = gerenadeSprite;
                        break;
                    case WeaponType.missleLuncher:
                        weapnDropsGOList[i].GetComponent<SpriteRenderer>().sprite = missleLuncherSprite;
                        break;
                    case WeaponType.plasma:
                        weapnDropsGOList[i].GetComponent<SpriteRenderer>().sprite = plasmaSprite;
                        break;
                    default:
                        break;
                }
            }
        }

        public void DespawnAllWeaponDrops()
        {
            for (int i = 0; i < weapnDropsGOList.Count; i++)
            {
                ObjectPool.Despawn(weapnDropsGOList[i]);
            }
            weapnDropsGOList = new List<GameObject>();
        }

        public void CollectWeaponDrop()
        {
            WeaponType resultWeaponType = WeaponType.NULL;
            for (int i = 0; i < weaponDropPosList.Count; i++)
            {
                if(Vector3.Distance(player_Controller.transform.position, weaponDropPosList[i]) < WEAPON_DROP_COLLECT_RADIUS)
                {
                    resultWeaponType = weaponDropTypeList[i];
                    player_Controller.SwitchToWeapon(resultWeaponType);
                    ObjectPool.Despawn(weapnDropsGOList[i]);
                    weapnDropsGOList.RemoveAt(i);
                    weaponDropPosList.RemoveAt(i);
                    weaponDropTypeList.RemoveAt(i);
                }
            }
        }
    }
}
