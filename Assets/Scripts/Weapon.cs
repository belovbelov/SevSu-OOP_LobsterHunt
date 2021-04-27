using Assets.Scripts.Entities;
using Assets.Scripts.ScriptableObjetsGenerator;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts
{
    public class Weapon : MonoBehaviour
    {
        #region Variables
        public Gun[] Loadout;
        public Transform WeaponParent;
        public Camera FpsCam;
        private GameObject currentWeapon;
        private int currentIndex;
        #endregion
        private void Start()
        {
            Equip(0);
        }
        private void Update()
        {
            if (currentWeapon != null)
            {
                if (!GameManager.GameIsPaused)
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

        private void Shoot()
        {
            if (Physics.Raycast(FpsCam.transform.position, FpsCam.transform.forward, out RaycastHit hit, Loadout[currentIndex].range))
            {
                var target = hit.transform.GetComponent<Fish>();
                if (target != null && target.InRange)
                {
                    target.TakeDamage(Loadout[currentIndex].damage);
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
