using UnityEngine;
using Fusion;
using Cinemachine;

public class CinemachineFollow : NetworkBehaviour
{
    private CinemachineFreeLook freeLookCamera;

    public override void Spawned()
    {
        if (Object.HasInputAuthority) // Chỉ thiết lập camera cho chính mình
        {
            freeLookCamera = FindObjectOfType<CinemachineFreeLook>();
            if (freeLookCamera != null)
            {
                freeLookCamera.Follow = transform;
                freeLookCamera.LookAt = transform;
            }
            else
            {
                Debug.LogError("Không tìm thấy Cinemachine FreeLook Camera trong scene!");
            }
        }
    }
}
