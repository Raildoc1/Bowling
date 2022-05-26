using Bowling.Input;
using UnityEngine;

namespace Bowling.Gameplay
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private float _verticalSpeed = 1.0f;
        [SerializeField] private float _horizontalSpeed = 2.5f;
        [SerializeField] private float _minX = -5.0f;
        [SerializeField] private float _maxX = 5.0f;
        [SerializeField] private InputListener _inputListener;

        private float _targetX;

        private void OnEnable()
        {
            _targetX = 0.0f;
            _inputListener.Drag += OnDrag;
        }

        private void OnDisable()
        {
            _inputListener.Drag -= OnDrag;
        }

        private void Update()
        {
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
