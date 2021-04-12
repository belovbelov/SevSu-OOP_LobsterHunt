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

        private void Awake()
        {
            slider = GetComponent<Slider>();
        }

        private void Update()
        {
            slider.value = 1 - player.timeInWater / 20.0f;
        }
    }
}