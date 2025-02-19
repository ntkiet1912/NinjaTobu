using UnityEngine;
using Cinemachine;

[ExecuteAlways]
[SaveDuringPlay]
[AddComponentMenu("Cinemachine/Extensions/Lock X Axis")]
public class LockXAxis : CinemachineExtension
{
    [SerializeField] private float lockedXPosition = 0f;
      
                      

    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage,
        ref CameraState state,
        float deltaTime)
    {
        
        if (stage == CinemachineCore.Stage.Body)
        {
            var pos = state.RawPosition;
            pos.x = lockedXPosition;
            state.RawPosition = pos;
        }
    }
}
