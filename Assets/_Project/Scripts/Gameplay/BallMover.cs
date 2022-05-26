using Bowling.Input;
using UnityEngine;
using UnityEngine.Pool;

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

        private IObjectPool<BallMover> _pool;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Init(Transform target, IObjectPool<BallMover> pool)
        {
            _target = target;
            _pool = pool;
            transform.localScale = Vector3.one * 0.1f;
        }

        private void Update()
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one, _balloonSpeed * Time.deltaTime);
        }

        private void FixedUpdate()
        {
            var currentPosition = transform.position;
            var displacement = _target.position - currentPosition;
            var direction = displacement.normalized;
            var desiredVelocity = direction.normalized * _speed;

            desiredVelocity *= displacement.magnitude * 0.05f;

            _rigidbody.velocity = desiredVelocity;
            //_rigidbody.velocity = Vector3.MoveTowards(_rigidbody.velocity, desiredVelocity, _acceleration * Time.deltaTime);
        }

        private void ReturnToPool()
        {
            _pool.Release(this);
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Obstacle"))
            {
                ReturnToPool();
            }
        }
    }
}