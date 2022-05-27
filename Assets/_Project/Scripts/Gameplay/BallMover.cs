using System.Collections;
using Bowling.Input;
using UnityEngine;
using UnityEngine.Pool;

namespace Bowling.Gameplay
{
    [RequireComponent(typeof(Rigidbody))]
    public class BallMover : MonoBehaviour
    {
        [SerializeField] private float _speed = 1.0f;
        [SerializeField] private float _balloonSpeed = 1.0f;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private MeshRenderer _view;

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

        private void OnEnable()
        {
            _view.gameObject.SetActive(true);
            _particleSystem.gameObject.SetActive(false);
        }

        public void Destroy()
        {
            StartCoroutine(DestroyRoutine());
        }

        private IEnumerator DestroyRoutine()
        {
            _view.gameObject.SetActive(false);
            _particleSystem.gameObject.SetActive(true);
            _particleSystem.Play();
            yield return new WaitForSeconds(_particleSystem.main.duration);
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