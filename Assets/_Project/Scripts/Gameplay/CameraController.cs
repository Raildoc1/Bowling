using Bowling.Gameplay;
using Cinemachine;
using UnityEngine;

namespace Bowling.Cinemachine
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _mainVirtualCamera;
        [SerializeField] private CinemachineVirtualCamera _finalVirtualCamera;

        private GameState _gameState;

        public void Init(GameState gameState)
        {
            _gameState = gameState;
            _gameState.GameStateChanged += OnGameStateChanged;
        }

        public void Deinit()
        {
            _gameState.GameStateChanged -= OnGameStateChanged;
        }

        private void OnGameStateChanged(GameState.State state)
        {
            switch (state)
            {
                case GameState.State.WaitingForTap:
                case GameState.State.Playing:
                case GameState.State.Lost:
                    _mainVirtualCamera.gameObject.SetActive(true);
                    _finalVirtualCamera.gameObject.SetActive(false);
                    break;
                case GameState.State.FinalCollecting:
                case GameState.State.FinalLaunching:
                case GameState.State.Win:
                    _mainVirtualCamera.gameObject.SetActive(false);
                    _finalVirtualCamera.gameObject.SetActive(true);
                    break;
            }
        }
    }
}