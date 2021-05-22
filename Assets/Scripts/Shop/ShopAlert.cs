using UnityEngine;

namespace Lobster.Shop
{
    public class ShopAlert : MonoBehaviour
    {
    
        public GameObject alertShopOb;
        public GameObject ShopMenuOb;
        public GameObject Oxygen;
        public GameObject Shop;
        public bool alertShop;
        public bool ShopIsOpen;
        void Start()
        {
            alertShop = false;
            alertShopOb = GameObject.Find("AlertShop");
            ShopMenuOb = GameObject.FindGameObjectWithTag("ShopMenu");
            Oxygen = GameObject.FindGameObjectWithTag("Oxygen");
            Shop = GameObject.Find("ShopManager");
            ShopMenuOb.SetActive(false);
            alertShopOb.SetActive(false);
        }
        private void OnTriggerEnter(Collider other)
        {
            alertShopOb.SetActive(true);
            alertShop = true;
        }
        private void OnTriggerExit(Collider other)
        {
            alertShopOb.SetActive(false);
            alertShop = false;
        
        }
    
        public void OpenShop()
        {
            //this.GetComponent<ShopManager>().money = Score.Instance.Amount;
            ShopMenuOb.SetActive(true);
            Oxygen.SetActive(false);
            ShopIsOpen = true;
            alertShopOb.SetActive(false);
            this.GetComponent<ShopManager>().FindObj();
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            //this.GetComponent<ButtonShop>().InitButtons();
        }
        public void CloseShop()
        {
            ShopMenuOb.SetActive(false);
            Oxygen.SetActive(true);
            ShopIsOpen = false;
            alertShopOb.SetActive(true);
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
