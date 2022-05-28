using Bowling.Gameplay;
using TMPro;
using UnityEngine;

namespace Bowling.View
{
    public class WinLoseWindowManager : MonoBehaviour
    {
        [SerializeField] private Transform _winWindow;
        [SerializeField] private Transform _loseWindow;
        [SerializeField] private TextMeshProUGUI _winText;

        private GameState _gameState;
        private PinsCounter _pinsCounter;

        private void Awake()
        {
            if (!_winWindow || !_loseWindow || !_winText)
            {
                Debug.LogError("WinLoseWindowManager: Wrong Description!");
            }
        }

        public void Init(GameState gameState, PinsCounter pinsCounter)
        {
            _gameState = gameState;
            _pinsCounter = pinsCounter;
            _gameState.GameStateChanged += OnChangeState;
        }

        public void Deinit()
        {
            _gameState.GameStateChanged -= OnChangeState;
        }

        private void OnChangeState(GameState.State state)
        {
            if (state == GameState.State.Win)
            {
                _winWindow.gameObject.SetActive(true);
                _winText.text = $"{_pinsCounter.FellPinsCount()}/{_pinsCounter.PinsCount()}";
                return;
            }

            if (state == GameState.State.Lost)
            {
                _loseWindow.gameObject.SetActive(true);
                return;
            }
        }

    }
}
