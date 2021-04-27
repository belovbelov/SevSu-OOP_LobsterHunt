using UnityEngine;

namespace Assets.Scripts
{
    public class Score : MonoBehaviour
    {//singleton pattern
        private static Score instance;
        public static Score Instance
        {
            get { return instance; }
        }

        public int Amount { get; set; }
        public int Fishkilled { get; set; }
        public float TimeSpent { get; set; }
        public int Deaths { get; set; }

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
            instance.Deaths = 0;
            instance.TimeSpent = 0;
            instance.Fishkilled = 0;
        }
    }
}