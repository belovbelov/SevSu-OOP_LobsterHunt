using UnityEngine;
using UnityEngine.UI;

namespace Lobster.UI
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