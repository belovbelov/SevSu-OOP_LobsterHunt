using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Assets.Scripts.Entities;

namespace Assets.Scripts.UI
{
    public class OxygenUI : MonoBehaviour
    {
        private Slider slider;

        private void Awake()
        {
            slider = GetComponent<Slider>();
        }

        public void SetSlider(float value)
        {
            slider.value = value;
        }
    }
}