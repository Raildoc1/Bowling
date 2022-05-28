using Bowling.Cinemachine;
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
        [SerializeField] private FinishTrigger _finishTrigger;
        [SerializeField] private FinalBallLauncher _finalBallLauncher;
        [SerializeField] private CameraController _cameraController;

        private void Awake()
        {
            _ballsHolder.Init(_gameState);
            _targetMover.Init(_inputListener, _gameState);
            _finishTrigger.Init(_gameState);
            _finalBallLauncher.Init(_inputListener, _gameState);
            _cameraController.Init(_gameState);
        }

        private void OnDestroy()
        {
            _ballsHolder.Deinit();
            _targetMover.Deinit();
            _finalBallLauncher.Deinit();
            _cameraController.Deinit();
        }
    }

}
