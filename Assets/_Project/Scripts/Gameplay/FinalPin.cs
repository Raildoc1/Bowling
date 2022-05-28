using UnityEngine;

namespace Bowling.Gameplay
{
    public class FinalPin : MonoBehaviour
    {
        [SerializeField] private float maxDistance = 1.0f;
        [SerializeField] private float maxAngle = 30.0f;

        private Vector3 _beginPosition;

        private void Awake()
        {
            _beginPosition = transform.position;
        }

        public bool Fell()
        {
            if (Vector3.Angle(transform.up, Vector3.up) > maxAngle)
            {
                return true;
            }

            return Vector3.Distance(transform.position, _beginPosition) > maxDistance;
        }
    }
}
