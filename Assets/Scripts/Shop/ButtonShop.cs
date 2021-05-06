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
                        btn.onClick.AddListener(() => shopManager.SetSpeed());
                        btn.onClick.AddListener(() => shopManager.PriceSpeed(25));
                        break;
                    case "Oxygen1":
                        btn.onClick.AddListener(() => shopManager.SetOxy());
                        btn.onClick.AddListener(() => shopManager.PriceOxy(25));
                        break;
                    case "Weapon1":
                        btn.onClick.AddListener(() => shopManager.SetWeap());
                        btn.onClick.AddListener(() => shopManager.PriceWeapon(25));
                        break;
                    default:
                        break;
                }

            }
        }
    }
}
