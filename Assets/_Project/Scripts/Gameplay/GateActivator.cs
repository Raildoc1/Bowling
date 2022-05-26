using System;
using UnityEngine;

namespace Bowling.Gameplay
{
    [RequireComponent(typeof(Collider))]
    public class GateActivator : MonoBehaviour
    {
        public enum OperationType
        {
            Add,
            Multiply,
            Subdivide,
        }

        [SerializeField] private BallsHolder _ballsHolder;
        [SerializeField] private GateActivator _oppositeGate;
        [SerializeField] private OperationType _operation;
        [Min(1)] [SerializeField] private int _value;

        private Collider _collider;

        public OperationType Operation => _operation;
        public float Value => _value;

        private void Awake()
        {
            if (!_ballsHolder)
            {
                _ballsHolder = FindObjectOfType<BallsHolder>();
            }

            _collider = GetComponent<Collider>();
        }

        private void Deactivate()
        {
            _collider.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Ball"))
            {
                return;
            }

            gameObject.SetActive(false);
            _oppositeGate.Deactivate();

            switch (_operation)
            {
                case OperationType.Add:
                    _ballsHolder.Add(_value);
                    break;
                case OperationType.Multiply:
                    _ballsHolder.Multiply(_value);
                    break;
                case OperationType.Subdivide:
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}