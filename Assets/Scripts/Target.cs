using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Target : MonoBehaviour
    {
        #region Variables
        public float health = 50f;
        #endregion
        #region Private methods
        public void TakeDamage(float amountDamage)
        {
            health -= amountDamage;
            if (health <= 0f)
            {
                Die();
            }
        }
        void Die()
        {
            //Дописать скрипт начисления очков+смерти объекта
            Destroy(gameObject);
        }
        #endregion
    }
}