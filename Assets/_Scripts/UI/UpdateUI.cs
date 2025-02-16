using Spawners.Factories;
using TMPro;
using UnityEngine;
using Zenject;

namespace UI
{
    public class UpdateUI : MonoBehaviour
    {
        [Header("Enemy Units UI")]
        [SerializeField] private TextMeshProUGUI _aliveEnemyUnitsText;
        [SerializeField] private TextMeshProUGUI _diedEnemyUnitsText;

        [Space, Header("Player Units UI")]
        [SerializeField] private TextMeshProUGUI _alivePlayerUnitsText;
        [SerializeField] private TextMeshProUGUI _diedPlayerUnitsText;
        
        /// <summary>
        /// Reference to the factory responsible for spawning and tracking enemy units.
        /// Used to update the enemy-related UI statistics.
        /// </summary>
        private EnemyUnitsFactory _enemyUnitsFactory;

        /// <summary>
        /// Reference to the factory responsible for spawning and tracking player units.
        /// Used to update the player-related UI statistics.
        /// </summary>
        private PlayerUnitsFactory _playerUnitsFactory;
        
        private int _diedEnemiesUnitsCount;
        private int _diedPlayerUnitsCount;
        
        private const string ALIVE_ENEMIES_UNITS_TEXT = "Alive: ";
        private const string DEATHS_ENEMIES_UNITS_TEXT = "Deaths: ";
        
        private const string ALIVE_PLAYER_UNITS_TEXT = "Alive: ";
        private const string DEATHS_PLAYER_UNITS_TEXT = "Deaths: ";
        
        [Inject]
        private void Construct(EnemyUnitsFactory enemyFactory, PlayerUnitsFactory playerFactory)
        {
            _enemyUnitsFactory = enemyFactory;
            _playerUnitsFactory = playerFactory;
        }

        private void Start()
        {
            ValidateComponents();
            
            UpdateAllText();
            SubscribeFactoriesEvents();
        }

        private void ValidateComponents()
        {
            if (!_diedEnemyUnitsText) Debug.LogWarning("No Text Component for: Died Enemy Units");
            if (!_aliveEnemyUnitsText) Debug.LogWarning("No Text Component for: Alive Enemy Units");
            
            if (!_diedPlayerUnitsText) Debug.LogWarning("No Text Component for: Died Player Units");
            if (!_alivePlayerUnitsText) Debug.LogWarning("No Text Component for: Alive Player Units");
            
            
            _enemyUnitsFactory = _enemyUnitsFactory ?? throw new MissingComponentException(
                "No EnemyUnitsFactory added! Do you have FactoryInstaller installed in the scene?");

            _playerUnitsFactory = _playerUnitsFactory ?? throw new MissingComponentException(
                "No EnemyUnitsFactory added! Check your FactoryInstaller!");
        }

        private void UpdateAllText()
        {
            // Enemy text
            _diedEnemyUnitsText.text = DEATHS_ENEMIES_UNITS_TEXT + _diedEnemiesUnitsCount;
            UpdateAliveEnemiesUnitsText(_enemyUnitsFactory.ActivePoolCapacity);
            
            // Player text
            _diedPlayerUnitsText.text = DEATHS_PLAYER_UNITS_TEXT + _diedPlayerUnitsCount;
            UpdateAlivePlayerUnitsText(_playerUnitsFactory.ActivePoolCapacity);
        }

        private void OnDestroy()
        {
            UnsubscribeFactoriesEvents();
        }

        #region Subscriptions
        
        private void SubscribeFactoriesEvents()
        {
            // Enemy UI subscriptions
            _enemyUnitsFactory.OnUnitSpawned += UpdateAliveEnemiesUnitsText;
            _enemyUnitsFactory.OnUnitDied += UpdateAliveEnemiesUnitsText;
            _enemyUnitsFactory.OnUnitDied += UpdateDiedEnemiesUnitsText;
            
            // Player UI subscriptions
            _playerUnitsFactory.OnUnitSpawned += UpdateAlivePlayerUnitsText;
            _playerUnitsFactory.OnUnitDied += UpdateAlivePlayerUnitsText;
            _playerUnitsFactory.OnUnitDied += UpdateDiedPlayerUnitsText;
        }

        private void UnsubscribeFactoriesEvents()
        {
            // Enemy UI subscriptions
            _enemyUnitsFactory.OnUnitSpawned -= UpdateAliveEnemiesUnitsText;
            _enemyUnitsFactory.OnUnitDied -= UpdateAliveEnemiesUnitsText;
            _enemyUnitsFactory.OnUnitDied -= UpdateDiedEnemiesUnitsText;
            
            // Player UI subscriptions
            _playerUnitsFactory.OnUnitSpawned -= UpdateAlivePlayerUnitsText;
            _playerUnitsFactory.OnUnitDied -= UpdateAlivePlayerUnitsText;
            _playerUnitsFactory.OnUnitDied -= UpdateDiedPlayerUnitsText;
        }
        
        #endregion

        #region Update Enemies Units UI
        
        private void UpdateDiedEnemiesUnitsText(int aliveCount)
        {
            _diedEnemiesUnitsCount++;
            _diedEnemyUnitsText.text = DEATHS_ENEMIES_UNITS_TEXT + _diedEnemiesUnitsCount;
        }

        private void UpdateAliveEnemiesUnitsText(int aliveCount)
        {
            _aliveEnemyUnitsText.text = ALIVE_ENEMIES_UNITS_TEXT + aliveCount;
        }
        
        #endregion

        #region Update Player Units UI
        
        private void UpdateDiedPlayerUnitsText(int aliveCount)
        {
            _diedPlayerUnitsCount++;
            _diedPlayerUnitsText.text = DEATHS_PLAYER_UNITS_TEXT + _diedPlayerUnitsCount;
        }

        private void UpdateAlivePlayerUnitsText(int aliveCount)
        {
            _alivePlayerUnitsText.text = ALIVE_PLAYER_UNITS_TEXT + aliveCount;
        }
        
        #endregion
    }
}