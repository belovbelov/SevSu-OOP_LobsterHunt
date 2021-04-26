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

        [SerializeField] public static int Amount { get; set; }
        public static int Fishkilled { get; set; }
        public static int TimeSpent { get; set; }
        public static int Deaths { get; set; }

        private void Awake()
        {
            Amount = 0;
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(this);
            }
        }
    }
}