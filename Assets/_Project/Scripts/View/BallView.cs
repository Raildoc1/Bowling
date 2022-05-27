using System;
using Bowling.Gameplay;
using UnityEngine;

namespace Bowling.View
{
    public class BallView : MonoBehaviour
    {
        [SerializeField] private BallMover _ballMover;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private MeshRenderer _view;

        private void OnEnable()
        {
            _ballMover.BallDestroyed += OnBallDestroyed;
            _view.gameObject.SetActive(true);
            _particleSystem.gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            _ballMover.BallDestroyed -= OnBallDestroyed;
        }

        private void OnBallDestroyed()
        {
            _view.gameObject.SetActive(false);
            _particleSystem.gameObject.SetActive(true);
            _particleSystem.Play();
        }
    }
}