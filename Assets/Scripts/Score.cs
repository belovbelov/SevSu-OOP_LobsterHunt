using Lobster.Entities;
using UnityEngine;

namespace Lobster
{
    public class Score : MonoBehaviour
    {//singleton pattern
        private static Score instance;
        public static Score Instance
        {
            get { return instance; }
        }

        private Score() {}
        
        public int Amount { get; set; }
        public int Fishkilled { get; set; }
        public float TimeSpent { get; set; }
        public int Deaths { get; set; }

        public int Speed { get; set; }
        
        public int Oxygen { get; set; }
        
        public int Weapon { get; set; }


        public void Zeros()
        {
            instance.Amount = 0;
            instance.Fishkilled = 0;
            instance.TimeSpent = 0;
            instance.Deaths = 0;
            instance.Oxygen = 1;
            instance.Weapon = 1;
            instance.Speed = 1;
        }
        public void UpdateScore(int deltaValue)
        {
            if (deltaValue == 0) return;
            Amount += deltaValue;
            Fishkilled += 1;
            GameManager.UpdateScore();
        }
        
        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(this);
                Zeros();
                
            }
            instance.Amount = 10000;          
        }
    }
}