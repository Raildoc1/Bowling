using System;
using Bowling.Gameplay;
using UnityEngine;
using UnityEngine.Serialization;

namespace Bowling.View
{
    public class BallView : MonoBehaviour
    {
        [FormerlySerializedAs("_ballMover")] [SerializeField] private Ball ball;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private MeshRenderer _view;

        private void Awake()
        {
            if (!ball || !_particleSystem || !_view)
            {
                Debug.LogError("BallView: Wrong Description!");
            }
        }

        private void OnEnable()
        {
            ball.BallDestroyed += OnBallDestroyed;
            _view.gameObject.SetActive(true);
            _particleSystem.gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            ball.BallDestroyed -= OnBallDestroyed;
        }

        private void OnBallDestroyed()
        {
            _view.gameObject.SetActive(false);
            _particleSystem.gameObject.SetActive(true);
            _particleSystem.Play();
        }
    }
}