using System.Collections.Generic;
using Bowling.Input;
using UnityEngine;

namespace Bowling.Gameplay
{
    public class BallsHolder : MonoBehaviour
    {
        [SerializeField] private int _initBallsAmount = 1;
        [SerializeField] private BallMover _ballPrefab;
        [SerializeField] private Transform _target;
        [SerializeField] private InputListener _inputListener;

        private readonly List<BallMover> _balls = new();

        private void Start()
        {
            SpawnAmount(_initBallsAmount);
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
                var ball = Instantiate(
                    _ballPrefab,
                    GetBallSpawnPosition(i),
                    Quaternion.identity,
                    transform);
                ball.Init(_target);
                _balls.Add(ball);
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
    }
}
