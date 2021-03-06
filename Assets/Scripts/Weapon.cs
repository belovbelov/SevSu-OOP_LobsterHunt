using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace weapon {
    public class Weapon : MonoBehaviour {
        #region Variables

        public Gun[] loadout;
        public Transform weaponParent;
        public Camera fpsCam;
        GameObject currentWeapon;
        int currentIndex;
        #endregion

        void Update() {
            if (currentWeapon != null) {
                if (Input.GetButtonDown("Fire1")) {
                    shoot();
                }
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha1)) { 
                equip(0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2)) {
                equip(1);
            }
            currentWeapon.transform.localPosition = Vector3.Lerp(currentWeapon.transform.localPosition, Vector3.zero, Time.deltaTime * 5f);
        }
        #region Private methods
        void shoot() {
            RaycastHit hit;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, loadout[currentIndex].range)) {
                Target target = hit.transform.GetComponent<Target>();
                if (target != null) {
                    target.takeDamage(loadout[currentIndex].damage);
                }
            }

            //Weapon FX
            currentWeapon.transform.position += currentWeapon.transform.forward * loadout[currentIndex].kickback;
        }

        void equip(int p_ind) {
            if (currentWeapon != null) {
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
