using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace Bowling.Gameplay
{
    [RequireComponent(typeof(Rigidbody))]
    public class Ball : MonoBehaviour
    {
        [SerializeField] private float _speed = 1.0f;
        [SerializeField] private float _balloonSpeed = 1.0f;
        [SerializeField] private float _destroyDelay = 0.2f;

        private Rigidbody _rigidbody;
        private Transform _target;
        private IObjectPool<Ball> _pool;

        public event Action BallDestroyed;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Init(Transform target, IObjectPool<Ball> pool)
        {
            _target = target;
            _pool = pool;
            transform.localScale = Vector3.one * 0.1f;
        }

        public void Destroy()
        {
            StartCoroutine(DestroyRoutine());
        }

        public void DisableGravity()
        {
            _rigidbody.useGravity = false;
            _rigidbody.constraints = RigidbodyConstraints.None;
        }

        private IEnumerator DestroyRoutine()
        {
            BallDestroyed?.Invoke();
            yield return new WaitForSeconds(_destroyDelay);
            _pool.Release(this);
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
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Obstacle"))
            {
                Destroy();
            }
        }
    }
}