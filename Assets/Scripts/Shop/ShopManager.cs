using Lobster.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace Lobster.Shop
{
    public class ShopManager : MonoBehaviour
    {
    
        [SerializeField] private int oxylvl=0;
        [SerializeField] private int speedlvl=0;
        [SerializeField] private int weaponlvl=0;
        [SerializeField] private int priceOxyMultiplier=1;
        [SerializeField] private int priceSpeedMultiplier=1;
        [SerializeField] private int priceWeaponMultiplier=1;


        [SerializeField] private int money;
        [SerializeField] private Player player;

    
        public GameObject stats;
    
        void Start()
        {
        
            player = GameObject.Find("Player").GetComponent<Player>();
        }
        public void FindObj()
        {
            stats = GameObject.Find("Stats1");
            stats.transform.Find("MoneyValue").GetComponent<Text>().text = Score.Instance.Amount.ToString();
        }
    
        public void SetSpeed()
        {
            speedlvl++;
        
            stats.transform.Find("Speedlvl").GetComponent<Text>().text = speedlvl.ToString();
            player.SwimModifier += speedlvl;
        }
        public void SetWeap()
        {
            weaponlvl++;
        
            stats.transform.Find("Weaponlvl").GetComponent<Text>().text = weaponlvl.ToString();
            //weapon = weaponlvl;
        }
        public void SetOxy()
        {
            oxylvl++;
        
            stats.transform.Find("Oxygenlvl").GetComponent<Text>().text = oxylvl.ToString();
            player.OxygenReduceRate *= oxylvl;
        }
   
        public void PriceOxy(int value)
        {
            var price = value * priceOxyMultiplier;
            Score.Instance.Amount -= price;
            priceOxyMultiplier++;
            stats.transform.Find("MoneyValue").GetComponent<Text>().text = Score.Instance.Amount.ToString();
        }
        public void PriceSpeed(int value)
        {
            var price = value * priceSpeedMultiplier;
            Score.Instance.Amount -= price;
            priceSpeedMultiplier++;
            stats.transform.Find("MoneyValue").GetComponent<Text>().text = Score.Instance.Amount.ToString();
        }
        public void PriceWeapon(int value)
        {
            var price = value * priceWeaponMultiplier;
            Score.Instance.Amount -= price;
            priceWeaponMultiplier++;
            stats.transform.Find("MoneyValue").GetComponent<Text>().text = Score.Instance.Amount.ToString();
        }



    }
}
