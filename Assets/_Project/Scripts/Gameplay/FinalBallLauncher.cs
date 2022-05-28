using System;
using Bowling.Input;
using DG.Tweening;
using UnityEngine;

namespace Bowling.Gameplay
{
    public class FinalBallLauncher : MonoBehaviour
    {
        [SerializeField] private FinalBall _finalBall;
        [SerializeField] private float _minAimAngle = -180.0f;
        [SerializeField] private float _maxAimAngle = 180.0f;
        [SerializeField] private float _aimMoveSensitivity = 0.01f;
        [SerializeField] private Transform _arrow;
        [SerializeField] private float _rotateTime = 1.0f;

        private InputListener _inputListener;
        private GameState _gameState;
        private float _aimAngle;
        private Sequence _sequence;
        private bool _finalStateStarted;

        public void Init(InputListener inputListener, GameState gameState)
        {
            _inputListener = inputListener;
            _gameState = gameState;

            _inputListener.Drag += OnPointerDrag;
            _inputListener.Up += OnPointerUp;
            _gameState.GameStateChanged += OnGameStateChanged;
        }

        public void Deinit()
        {
            _inputListener.Drag -= OnPointerDrag;
            _inputListener.Up -= OnPointerUp;
            _gameState.GameStateChanged -= OnGameStateChanged;
        }

        private void OnGameStateChanged(GameState.State gameState)
        {
            if ((gameState != GameState.State.Final) || _finalStateStarted)
            {
                return;
            }
            _finalStateStarted = true;
            _aimAngle = _minAimAngle;
            _sequence = DOTween.Sequence();
            _sequence.Append(DOTween.To(() => _aimAngle, x =>
            {
                _aimAngle = x;
                _arrow.rotation = Quaternion.Euler(new Vector3(0.0f, _aimAngle, 0.0f));
            }, _maxAimAngle, _rotateTime));
            _sequence.Append(DOTween.To(() => _aimAngle, x =>
            {
                _aimAngle = x;
                _arrow.rotation = Quaternion.Euler(new Vector3(0.0f, _aimAngle, 0.0f));
            }, _minAimAngle, _rotateTime));
            _sequence.SetLoops(-1, LoopType.Restart);
            _sequence.SetEase(Ease.Linear);
            _sequence.Play();
            Debug.Log("Play");
        }

        private void OnPointerDrag(float delta)
        {
            if (_gameState.CurrentState != GameState.State.Final)
            {
                return;
            }

            _aimAngle += delta * _aimMoveSensitivity;
            _aimAngle = Mathf.Clamp(_aimAngle, _minAimAngle, _maxAimAngle);
            _arrow.rotation = Quaternion.Euler(new Vector3(0.0f, _aimAngle, 0.0f));
        }

        private void OnPointerUp()
        {
            if (_gameState.CurrentState != GameState.State.Final)
            {
                return;
            }
        }
    }
}