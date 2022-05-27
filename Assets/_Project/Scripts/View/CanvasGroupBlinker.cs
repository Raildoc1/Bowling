using DG.Tweening;
using UnityEngine;

namespace Bowling.View
{
    [RequireComponent(typeof(CanvasGroup))]
    public class CanvasGroupBlinker : MonoBehaviour
    {
        [SerializeField] private float _fadeTime = 1.0f;
        private CanvasGroup _canvasGroup;
        private Sequence _sequence;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _sequence = DOTween.Sequence();
            _sequence.Append(_canvasGroup.DOFade(0.0f, _fadeTime));
            _sequence.Append(_canvasGroup.DOFade(1.0f, _fadeTime));
            _sequence.SetLoops(-1, LoopType.Restart);
        }

        private void OnEnable()
        {
            _sequence.Play();
        }
    }
}