using UnityEngine;

namespace Model
{
    public class GameSession : MonoBehaviour
    {
        // Using this, we can add more stuff to save
        // That's the reason, why I don't use PlayerPrefs instead
        [SerializeField] private PlayerData _data;
        public PlayerData Data => _data;

        private void Awake()
        {
            if (IsSessionExist())
            {
                DestroyImmediate(gameObject);
            }
            else
            {
                DontDestroyOnLoad(this);
            }
        }

        private bool IsSessionExist()
        {
            var sessions = FindObjectsOfType<GameSession>();

            foreach (var session in sessions)
            {
                if (session != this) return true;
            }
            return false;
        }

        public void SaveDesiredEnemyUnits(int desiredUnits)
        {
            if(desiredUnits < 0) return;
            _data.DesiredEnemyUnitsOnStart = desiredUnits;
        }
        
        public void SaveDesiredEnemyUnits(string desiredUnitsText)
        {
            if (!int.TryParse(desiredUnitsText, out var desiredUnitsNumber))
            {
                Debug.LogWarning("Trying to save string, not number!");
                return;
            }
            
            SaveDesiredEnemyUnits(desiredUnitsNumber);
        }
    }
}