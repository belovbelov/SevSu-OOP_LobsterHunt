using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace weapon
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
                if (Input.GetButtonDown("Fire1"))
                {
                    Shoot();
                }
                currentWeapon.transform.localPosition = Vector3.Lerp(currentWeapon.transform.localPosition, Vector3.zero, Time.deltaTime * 5f);
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Equip(0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Equip(1);
            }
        }
        #region Private methods
        void Shoot()
        {
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out RaycastHit hit, loadout[currentIndex].range))
            {
                Target target = hit.transform.GetComponent<Target>();
                if (target != null)
                {
                    target.takeDamage(loadout[currentIndex].damage);
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
