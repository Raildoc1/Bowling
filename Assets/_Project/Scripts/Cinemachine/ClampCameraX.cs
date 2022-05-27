using UnityEngine;
using Cinemachine;

namespace Bowling.Cinemachine
{
    [ExecuteInEditMode] [SaveDuringPlay] [AddComponentMenu("")]
    public class ClampCameraX : CinemachineExtension
    {
        [SerializeField] private float _minX = -1.0f;
        [SerializeField] private float _maxX = 1.0f;

        protected override void PostPipelineStageCallback(
            CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            if (stage == CinemachineCore.Stage.Body)
            {
                var pos = state.RawPosition;
                pos.x = Mathf.Clamp(pos.x, _minX, _maxX);
                state.RawPosition = pos;
            }
        }
    }
}