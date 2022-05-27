using UnityEngine;

namespace Bowling.Gameplay
{
    [RequireComponent(typeof(Collider))]
    public class FinalBall : MonoBehaviour
    {
        private int _strength = 0;

        [SerializeField] private float _maxBalls = 100.0f;
        [SerializeField] private float _maxScale = 5.0f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Ball"))
            {
                other.gameObject.GetComponent<BallMover>().Destroy();
                _strength++;
                transform.localScale = Vector3.one * (_strength * (_maxScale - 1.0f) / _maxBalls + 1.0f);
            }
        }
    }
}