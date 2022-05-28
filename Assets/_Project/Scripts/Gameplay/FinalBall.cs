using UnityEngine;

namespace Bowling.Gameplay
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class FinalBall : MonoBehaviour
    {
        private int _mass = 0;
        private Rigidbody _rigidbody;

        [SerializeField] private float _maxBalls = 100.0f;
        [SerializeField] private float _maxScale = 5.0f;
        [SerializeField] private Collider _collider;
        [SerializeField] private Collider _trigger;
        [SerializeField] private Transform _mesh;
        [SerializeField] private float _launchStrength = 100.0f;

        private void Awake()
        {
            _mesh.localScale = _mass == 0 ? Vector3.zero : Vector3.one;
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Launch(Vector3 direction)
        {
            _trigger.enabled = false;
            _collider.enabled = true;
            _rigidbody.AddForce(direction * _launchStrength, ForceMode.Impulse);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Ball"))
            {
                other.gameObject.GetComponent<BallMover>().Destroy();
                _mass++;
                _rigidbody.mass = _mass;
                _mesh.localScale = _mass == 0 ? Vector3.zero : Vector3.one;
                transform.localScale = Vector3.one * (_mass * (_maxScale - 1.0f) / _maxBalls + 1.0f);
            }
        }
    }
}