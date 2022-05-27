using Bowling.Input;
using UnityEngine;

namespace Bowling.Gameplay
{
    public class GameStarter : MonoBehaviour
    {
        [SerializeField] private BallsHolder _ballsHolder;
        [SerializeField] private TargetMover _targetMover;
        [SerializeField] private GameState _gameState;
        [SerializeField] private InputListener _inputListener;

        private void Awake()
        {
            _ballsHolder.Init(_gameState);
            _targetMover.Init(_inputListener, _gameState);
        }

        private void OnDestroy()
        {
            _ballsHolder.Deinit();
            _targetMover.Deinit();
        }
    }

}
