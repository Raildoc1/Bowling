using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Bowling.Input
{
    public class InputListener : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerMoveHandler
    {
        [SerializeField] private float _inputSensitivity = 0.1f;

        private bool _dragging = false;

        public event Action<float> Drag;
        public event Action Up;

        public void OnPointerDown(PointerEventData eventData)
        {
            _dragging = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _dragging = false;
            Up?.Invoke();
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            if (_dragging)
            {
                Drag?.Invoke(eventData.delta.x * _inputSensitivity);
            }
        }
    }
}