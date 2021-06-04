using Lobster.Entities;
using Lobster.ScriptableObjetsGenerator;
using Lobster.Shop;
using UnityEngine;

namespace Lobster
{
    public class Weapon : MonoBehaviour
    {
        #region Variables
        public Gun[] Loadout;
        public Transform WeaponParent;
        public Camera FpsCam;
        private GameObject currentWeapon;
        private int currentIndex;
        
        //костыль, надо делать что то
        private bool ableToShoot;
        #endregion
        public void ChangeWeapon(int level)
        {
            Equip(--level);
        }
        private void Start()
        {
            Equip(0);
        }
        private void Update()
        {
            //жесть
            ableToShoot = (!GameManager.Instance.GameIsPaused || GameManager.Instance.shop.ShopIsOpen) &&
                          (GameManager.Instance.GameIsPaused || !GameManager.Instance.shop.ShopIsOpen);
            if (currentWeapon != null)
            {
                if (ableToShoot)
                {
                    if (Input.GetButtonDown("Fire1"))
                    {
                        Shoot();
                    }
                    currentWeapon.transform.localPosition = Vector3.Lerp(currentWeapon.transform.localPosition, Vector3.zero, Time.deltaTime * 5f/Score.Instance.Weapon);
                }
            }
            
        }
        #region Private methods

        // ReSharper disable Unity.PerformanceAnalysis
        private void Shoot()
        {
            if (Physics.Raycast(FpsCam.transform.position, FpsCam.transform.forward, out RaycastHit hit, Loadout[currentIndex].range))
            {
                var target = hit.transform.GetComponent<Fish>();
                if (target != null && target.InRange)
                {
                    Score.Instance.UpdateScore(target.TakeDamage(Loadout[currentIndex].damage));
                }
            }

            //Weapon FX
            currentWeapon.transform.position += currentWeapon.transform.forward * Loadout[currentIndex].kickback;
        }

        private void Equip(int pInd)
        {
            if (currentWeapon != null)
            {
                Destroy(currentWeapon);
            }

            currentIndex = pInd;

            var newWeapon = Instantiate(Loadout[pInd].prefab, WeaponParent.position, WeaponParent.rotation, WeaponParent) as GameObject;
            newWeapon.transform.localPosition = Vector3.zero;
            currentWeapon = newWeapon;
        }
        #endregion
    }
}
