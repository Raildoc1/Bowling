using Bowling.Input;
using UnityEngine;

namespace Bowling.Gameplay
{
    [RequireComponent(typeof(Rigidbody))]
    public class BallMover : MonoBehaviour
    {
        [SerializeField] private float _speed = 1.0f;
        [SerializeField] private float _acceleration = 1.0f;
        [SerializeField] private float _balloonSpeed = 1.0f;

        private Rigidbody _rigidbody;
        private Transform _target;
        private InputListener _inputListener;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Init(Transform target)
        {
            _target = target;
            transform.localScale = Vector3.one * 0.1f;
        }

        private void Update()
        {
            var currentPosition = transform.position;
            var displacement = _target.position - currentPosition;
            var direction = displacement.normalized;
            var desiredVelocity = direction.normalized * _speed;

            desiredVelocity *= displacement.magnitude * 0.05f;

            _rigidbody.velocity = Vector3.MoveTowards(_rigidbody.velocity, desiredVelocity, _acceleration * Time.deltaTime);
            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one, _balloonSpeed * Time.deltaTime);
        }
    }
}