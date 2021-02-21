using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {
    #region Variables
    public float health = 50f;
    #endregion
    #region Private methods
    public void takeDamage(float amountDamage) {
        health -= amountDamage;
        if (health <= 0f) {
            die();
        }
    }
    void die() {
        //Дописать скрипт начисления очков+смерти объекта
        Destroy(gameObject);
    }
    #endregion
}
