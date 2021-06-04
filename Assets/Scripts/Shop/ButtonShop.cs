using UnityEngine;
using UnityEngine.UI;

namespace Lobster.Shop
{
    public class ButtonShop : MonoBehaviour
    {
        public ShopManager shopManager;
        void Start()
        {   //include script ShopManager
            shopManager = GameObject.Find("ShopManager").GetComponent<ShopManager>();
            Button[] btns = Resources.FindObjectsOfTypeAll<Button>();

            foreach (var btn in btns)
            {
                switch (btn.gameObject.name)
                {
                    case "Speed1":
                        btn.onClick.AddListener(() => shopManager.UpdateSpeedLevel());
                        break;
                    case "Oxygen1":
                        btn.onClick.AddListener(() => shopManager.UpdateOxyLevel());
                        break;
                    case "Weapon1":
                        btn.onClick.AddListener(() => shopManager.UpdateWeaponLevel());
                        break;
                    case "OxygenBar":
                        btn.GetComponent<Image>().sprite = Resources.Load<Sprite>("aqua"+ shopManager.getoxy());
                        shopManager.OxygenBar = btn.gameObject; // Смена текстур в отдельный класс
                        break;
                    case "SpeedBar":
                        btn.GetComponent<Image>().sprite = Resources.Load<Sprite>("fleepers"+ shopManager.getspeed());
                        shopManager.SpeedBar = btn.gameObject;
                        break;
                    case "WeaponBar":
                        btn.GetComponent<Image>().sprite = Resources.Load<Sprite>("harpoon" + shopManager.getweapon());
                        shopManager.WeaponBar=btn.gameObject;
                        break;
                    default:
                        break;
                }

            }
        }
    }
}
