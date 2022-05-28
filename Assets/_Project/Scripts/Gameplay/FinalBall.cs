using UnityEngine;

namespace Bowling.Gameplay
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class FinalBall : MonoBehaviour
    {
        private int _strength = 0;
        private Rigidbody _rigidbody;

        [SerializeField] private float _maxBalls = 100.0f;
        [SerializeField] private float _maxScale = 5.0f;
        [SerializeField] private Collider _collider;
        [SerializeField] private Collider _trigger;
        [SerializeField] private Transform _mesh;

        private void Awake()
        {
            _mesh.localScale = _strength == 0 ? Vector3.zero : Vector3.one;
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void PrepareForLaunch()
        {
            _trigger.enabled = false;
            _collider.enabled = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Ball"))
            {
                other.gameObject.GetComponent<BallMover>().Destroy();
                _strength++;
                _rigidbody.mass = _strength;
                _mesh.localScale = _strength == 0 ? Vector3.zero : Vector3.one;
                transform.localScale = Vector3.one * (_strength * (_maxScale - 1.0f) / _maxBalls + 1.0f);
            }
        }
    }
}