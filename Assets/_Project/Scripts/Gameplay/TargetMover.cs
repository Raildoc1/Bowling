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

        private InputListener _inputListener;
        private GameState _gameState;
        private float _targetX;

        public void Init(InputListener inputListener, GameState gameState)
        {
            _inputListener = inputListener;
            _targetX = 0.0f;
            _inputListener.Drag += OnDrag;
            _gameState = gameState;
        }

        public void Deinit()
        {
            _inputListener.Drag -= OnDrag;
        }

        private void Update()
        {
            if (!_gameState)
            {
                return;
            }

            if (_gameState.CurrentState == GameState.State.Lost
                || _gameState.CurrentState == GameState.State.WaitingForTap)
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
    }
}
