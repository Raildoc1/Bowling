using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Bowling.Input
{
    public class InputListener : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerMoveHandler, IPointerClickHandler
    {
        [SerializeField] private float _inputSensitivity = 0.1f;

        private bool _dragging = false;

        public event Action<float> Drag;
        public event Action Click;

        public void OnPointerDown(PointerEventData eventData)
        {
            _dragging = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _dragging = false;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Click?.Invoke();
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