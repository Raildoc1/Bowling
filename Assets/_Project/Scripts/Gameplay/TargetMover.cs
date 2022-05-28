using Bowling.Input;
using UnityEngine;

namespace Bowling.Gameplay
{
    public class TargetMover : MonoBehaviour
    {
        [SerializeField] private float _verticalSpeed = 1.0f;
        [SerializeField] private float _horizontalSpeed = 2.5f;
        [SerializeField] private float _minX = -5.0f;
        [SerializeField] private float _maxX = 5.0f;
        [SerializeField] private Transform _finalTarget;

        private InputListener _inputListener;
        private GameState _gameState;
        private float _targetX;
        private bool _movingForward;

        public void Init(InputListener inputListener, GameState gameState)
        {
            _inputListener = inputListener;
            _gameState = gameState;
            _targetX = 0.0f;
            _inputListener.Drag += OnDrag;
            _gameState.GameStateChanged += OnGameStateChanged;
        }

        public void Deinit()
        {
            _inputListener.Drag -= OnDrag;
            _gameState.GameStateChanged -= OnGameStateChanged;
        }

        private void Update()
        {
            if (!_movingForward)
            {
                return;
            }

            var currentPosition = transform.position;
            var targetPosition = currentPosition;
            targetPosition.x = _targetX;
            currentPosition = Vector3.MoveTowards(currentPosition, targetPosition, _horizontalSpeed * 2.5f * Time.deltaTime);
            transform.position = Vector3.MoveTowards(currentPosition, currentPosition + Vector3.forward, _verticalSpeed * Time.deltaTime);
        }

        private void OnDrag(float delta)
        {
            _targetX = Mathf.Clamp(_targetX + delta, _minX, _maxX);
        }

        private void OnGameStateChanged(GameState.State state)
        {
            _movingForward = state == GameState.State.Playing;

            if (state == GameState.State.FinalCollecting)
            {
                transform.position = _finalTarget.position;
            }
        }
    }
}
