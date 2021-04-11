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
        public Player player;

        public float FillSpeed { get; set; } = 0.5f;
        private float targetProgress = 1.0f;

        private void Awake()
        {
            slider = GetComponent<Slider>();
        }

        private void Update()
        {
            slider.value = 1 - player.timeInWater / 60.0f;
        }
    }
}