using UnityEngine;

namespace Bowling.Gameplay
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class Pin : MonoBehaviour
    {
        [SerializeField] private float _hitStrength = 10.0f;
        [SerializeField] private float _upHitStrength = 2.0f;

        private Rigidbody _rigidbody;
        private bool _hit;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_hit || !collision.gameObject.CompareTag("Ball"))
            {
                return;
            }

            var ball = collision.gameObject.GetComponent<BallMover>();

            if (!ball.isActiveAndEnabled)
            {
                return;
            }

            _rigidbody.AddForce(collision.contacts[0].normal * _hitStrength + Vector3.up * _upHitStrength, ForceMode.Impulse);
            collision.gameObject.GetComponent<BallMover>().Destroy();
            _hit = true;
        }
    }
}
