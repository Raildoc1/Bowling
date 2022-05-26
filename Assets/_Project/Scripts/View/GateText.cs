using System;
using Bowling.Gameplay;
using TMPro;
using UnityEngine;

namespace Bowling.View
{
    [RequireComponent(typeof(TextMeshPro))]
    public class GateText : MonoBehaviour
    {
        [SerializeField] private GateActivator _gateActivator;

        private TextMeshPro _textMesh;

        private void Awake()
        {
            _textMesh = GetComponent<TextMeshPro>();
            switch (_gateActivator.Operation)
            {
                case GateActivator.OperationType.Add:
                    _textMesh.text = $"+{_gateActivator.Value}";
                    break;
                case GateActivator.OperationType.Multiply:
                    _textMesh.text = $"x{_gateActivator.Value}";
                    break;
                case GateActivator.OperationType.Subdivide:
                    _textMesh.text = $"-{_gateActivator.Value}";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}