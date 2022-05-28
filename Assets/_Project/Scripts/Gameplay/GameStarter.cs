using Bowling.Cinemachine;
using Bowling.Input;
using Bowling.View;
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
        [SerializeField] private WinLoseWindowManager _winLoseWindowManager;
        [SerializeField] private PinsCounter _pinsCounter;

        private void Awake()
        {
            _ballsHolder.Init(_gameState);
            _targetMover.Init(_inputListener, _gameState);
            _finishTrigger.Init(_gameState);
            _finalBallLauncher.Init(_inputListener, _gameState);
            _cameraController.Init(_gameState);
            _winLoseWindowManager.Init(_gameState, _pinsCounter);
        }

        private void OnDestroy()
        {
            _ballsHolder.Deinit();
            _targetMover.Deinit();
            _finalBallLauncher.Deinit();
            _cameraController.Deinit();
            _winLoseWindowManager.Deinit();
        }
    }

}
