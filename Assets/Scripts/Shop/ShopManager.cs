using System.Runtime.CompilerServices;
using Lobster.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace Lobster.Shop
{
    public class ShopManager : MonoBehaviour
    {
    
        [SerializeField] private int oxylvl=1;
        [SerializeField] private int speedlvl=1;
        [SerializeField] private  int weaponlvl=1;
        [SerializeField] private int priceOxy=25;
        [SerializeField] private int priceSpeed=25;
        [SerializeField] private int priceWeapon=25;

        private static int levelsSum;
        [SerializeField] private Player player;
        [SerializeField] public GameObject WeaponBar;

    
        public GameObject stats;
    
        void Start()
        {
            player = GameObject.Find("Player").GetComponent<Player>();
        }
        public void FindObj()
        {
            stats = GameObject.Find("Stats1");
            stats.transform.Find("MoneyValue").GetComponent<Text>().text = Score.Instance.Amount.ToString();
            stats.transform.Find("Speedlvl").GetComponent<Text>().text = Score.Instance.Speed.ToString();
            stats.transform.Find("Weaponlvl").GetComponent<Text>().text = Score.Instance.Weapon.ToString();
            stats.transform.Find("Oxygenlvl").GetComponent<Text>().text = Score.Instance.Oxygen.ToString();
        }
    
        public void SetSpeed()
        {
            speedlvl++;
        
            stats.transform.Find("Speedlvl").GetComponent<Text>().text = speedlvl.ToString();
            player.SwimModifier += speedlvl;
            Score.Instance.Speed = speedlvl;
        }
        public void SetWeap()
        {
            weaponlvl++;
        
            stats.transform.Find("Weaponlvl").GetComponent<Text>().text = weaponlvl.ToString();
            //weapon = weaponlvl;
            Score.Instance.Weapon = weaponlvl;
            WeaponBar.GetComponent<Image>().sprite = Resources.Load<Sprite>("harpoon" + weaponlvl);
        }
        public void SetOxy()
        {
            oxylvl++;
        
            stats.transform.Find("Oxygenlvl").GetComponent<Text>().text = oxylvl.ToString();
            player.OxygenReduceRate *= oxylvl;
            Score.Instance.Oxygen = oxylvl;
        }
   
        public void PriceOxy()
        {
            Score.Instance.Amount -= priceOxy;
            priceOxy+= priceOxy;
            stats.transform.Find("MoneyValue").GetComponent<Text>().text = Score.Instance.Amount.ToString();
            GameObject.Find("Oxygen1").GetComponentInChildren<Text>().text = "Up\n" + priceOxy;
        }
        public void PriceSpeed()
        {
            Score.Instance.Amount -= priceSpeed;
            priceSpeed+= priceSpeed;
            stats.transform.Find("MoneyValue").GetComponent<Text>().text = Score.Instance.Amount.ToString();
            GameObject.Find("Speed1").GetComponentInChildren<Text>().text = "Up\n" + priceSpeed;
        }
        public void PriceWeapon()
        {
            Score.Instance.Amount -= priceWeapon;
            priceWeapon+= priceWeapon;
            stats.transform.Find("MoneyValue").GetComponent<Text>().text = Score.Instance.Amount.ToString();
            GameObject.Find("Weapon1").GetComponentInChildren<Text>().text = "Up\n" + priceWeapon;
        }

        public void UpdateSpeedLevel()
        {
            if (Score.Instance.Amount < priceSpeed)
            {
                return;
            }
            SetSpeed();
            PriceSpeed();
            GameManager.UpdateScore();
            Sum();
        }
        public void UpdateOxyLevel()
        {
            if (Score.Instance.Amount < priceOxy)
            {
                return;
            }
            SetOxy();
            PriceOxy();
            GameManager.UpdateScore();
            Sum();
        }
        
        public void UpdateWeaponLevel()
        {
            if (Score.Instance.Amount < priceWeapon)
            {
                return;
            }
            SetWeap();
            PriceWeapon();
            GameManager.UpdateScore();
            Sum();
        }

        private int Sum()
        {
            levelsSum = oxylvl + weaponlvl + speedlvl;
            return levelsSum;
        }
        public static int LevelsSum()
        {
            return  levelsSum;
        }
    }
}
