using UnityEngine;
using UnityEngine.UI;

namespace Lobster.Shop
{
    public class ShopManager : MonoBehaviour
    {
    
        [SerializeField] private int oxygen;
        [SerializeField] private int speed;
        [SerializeField] private int weapon;
        [SerializeField] private int oxylvl=0;
        [SerializeField] private int speedlvl=0;
        [SerializeField] private int weaponlvl=0;
        [SerializeField] private int priceOxy=1;
        [SerializeField] private int priceSpeed=1;
        [SerializeField] private int priceWeapon=1;


        [SerializeField] private int money;

        public GameObject WeaponBar;
        public GameObject stats;
    
        void Start()
        {
        
            money = Score.Instance.Amount;
        
        }
        public void FindObj()
        {
            stats = GameObject.Find("Stats1");
            stats.transform.Find("MoneyValue").GetComponent<Text>().text = Score.Instance.Amount.ToString();
        }
    
        //public void GetMoney

        public void SetSpeed()
        {
            speedlvl++;
        
            stats.transform.Find("Speedlvl").GetComponent<Text>().text = speedlvl.ToString();
            speed = speedlvl;
        }
        public void SetWeap()
        {
            weaponlvl++;
        
            stats.transform.Find("Weaponlvl").GetComponent<Text>().text = weaponlvl.ToString();
            weapon = weaponlvl;
            WeaponBar.GetComponent<Image>().sprite = Resources.Load<Sprite>("harpon2");
        }
        public void SetOxy()
        {
            oxylvl++;
        
            stats.transform.Find("Oxygenlvl").GetComponent<Text>().text = oxylvl.ToString();
            oxygen = oxylvl;
        }
   
        public void PriceOxy(int value)
        {
            Score.Instance.Amount = Score.Instance.Amount - value * priceOxy;
            priceOxy++;
            stats.transform.Find("MoneyValue").GetComponent<Text>().text = Score.Instance.Amount.ToString();
        }
        public void PriceSpeed(int value)
        {
            Score.Instance.Amount = Score.Instance.Amount - value * priceSpeed;
            priceSpeed++;
            stats.transform.Find("MoneyValue").GetComponent<Text>().text = Score.Instance.Amount.ToString();
        }
        public void PriceWeapon(int value)
        {
            Score.Instance.Amount = Score.Instance.Amount - value * priceWeapon;
            priceWeapon++;
            stats.transform.Find("MoneyValue").GetComponent<Text>().text = Score.Instance.Amount.ToString();
        }



    }
}
