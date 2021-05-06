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
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(this);
            }

            instance.Amount = 0;
            instance.Fishkilled = 0;
            instance.TimeSpent = 0;
            instance.Deaths = 0;
        }
    }
}