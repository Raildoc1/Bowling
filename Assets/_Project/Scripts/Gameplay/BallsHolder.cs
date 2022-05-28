using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace Bowling.Gameplay
{
    public class BallsHolder : MonoBehaviour
    {
        [SerializeField] private int _initBallsAmount = 1;
        [SerializeField] private BallMover _ballPrefab;
        [SerializeField] private Transform _target;

        private readonly List<BallMover> _balls = new();
        private IObjectPool<BallMover> _pool;
        private GameState _gameState;

        public void Init(GameState gameState)
        {
            _gameState = gameState;
            _gameState.GameStateChanged += OnGameStateChanged;
            _pool = new ObjectPool<BallMover>(
                CreateBall,
                OnTakeFromPool,
                OnReturnedToPool,
                OnDestroyPoolObject,
                true,
                50,
                200);
        }

        public void Deinit()
        {
            _gameState.GameStateChanged -= OnGameStateChanged;
        }

        private void OnGameStateChanged(GameState.State state)
        {
            switch (state)
            {
                case GameState.State.Playing:
                    SpawnAmount(_initBallsAmount);
                    break;
                case GameState.State.FinalCollecting:
                    foreach (var ballMover in _balls)
                    {
                        ballMover.DisableGravity();
                    }
                    break;
                case GameState.State.WaitingForTap:
                case GameState.State.Lost:
                    break;
            }
        }

        public void Multiply(int value)
        {
            SpawnAmount(_balls.Count * (value - 1));
        }

        public void Add(int value)
        {
            SpawnAmount(value);
        }

        private void SpawnAmount(int amount)
        {
            for (var i = 0; i < amount; i++)
            {
                var ball = _pool.Get();
                var ballTransform = ball.transform;
                ballTransform.position = GetBallSpawnPosition(i);
                ballTransform.rotation = Quaternion.identity;
            }
        }

        private Vector3 GetBallSpawnPosition(int index)
        {
            if (_balls.Count == 0)
            {
                return transform.position;
            }
            var offset = Random.Range(-1.0f, 1.0f) * Vector3.forward + Random.Range(-1.0f, 1.0f) * Vector3.right;
            return _balls[index % _balls.Count].transform.position + offset.normalized * 0.01f;
        }

        private BallMover CreateBall()
        {
            var ball = Instantiate(
                _ballPrefab,
                transform.position,
                Quaternion.identity,
                transform);
            ball.Init(_target, _pool);
            return ball;
        }

        private void OnReturnedToPool(BallMover ball)
        {
            _balls.Remove(ball);
            ball.gameObject.SetActive(false);
            if (_balls.Count == 0 )
            {
                switch (_gameState.CurrentState)
                {
                    case GameState.State.FinalCollecting:
                        _gameState.StartLaunching();
                        break;
                    case GameState.State.Playing:
                        _gameState.LoseGame();
                        break;
                    case GameState.State.WaitingForTap:
                    case GameState.State.Lost:
                    case GameState.State.FinalLaunching:
                    case GameState.State.Win:
                        break;
                }
            }
        }

        private void OnTakeFromPool(BallMover ball)
        {
            _balls.Add(ball);
            ball.gameObject.SetActive(true);
        }

        private void OnDestroyPoolObject(BallMover ball)
        {
            _balls.Remove(ball);
            Destroy(ball.gameObject);
            if (_balls.Count == 0)
            {
                _gameState.LoseGame();
            }
        }
    }
}
