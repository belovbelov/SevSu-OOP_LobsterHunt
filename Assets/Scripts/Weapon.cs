using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.ScriptableObjetsGenerator;
using Assets.Scripts.UI;
using Assets.Scripts.Entities;

namespace Assets.Scripts
{
    public class Weapon : MonoBehaviour
    {
        #region Variables

        public Gun[] loadout;
        public Transform weaponParent;
        public Camera fpsCam;
        GameObject currentWeapon;
        int currentIndex;
        #endregion

        private void Update()
        {
            if (currentWeapon != null)
            {
                if (!InGameMenu.GameIsPaused)
                {
                    if (Input.GetButtonDown("Fire1"))
                    {
                        Shoot();
                    }
                    currentWeapon.transform.localPosition = Vector3.Lerp(currentWeapon.transform.localPosition, Vector3.zero, Time.deltaTime * 5f);
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Equip(0);
            }
        }
        #region Private methods
        void Shoot()
        {
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out RaycastHit hit, loadout[currentIndex].range))
            {
                Fish target = hit.transform.GetComponent<Fish>();
                if (target != null && target.inRange)
                {
                    target.TakeDamage(loadout[currentIndex].damage);
                }
            }

            //Weapon FX
            currentWeapon.transform.position += currentWeapon.transform.forward * loadout[currentIndex].kickback;
        }

        private void Equip(int p_ind)
        {
            if (currentWeapon != null)
            {
                Destroy(currentWeapon);
            }

            currentIndex = p_ind;

            GameObject newWeapon = Instantiate(loadout[p_ind].prefab, weaponParent.position, weaponParent.rotation, weaponParent) as GameObject;
            newWeapon.transform.localPosition = Vector3.zero;
            currentWeapon = newWeapon;
        }
        #endregion
    }
}
