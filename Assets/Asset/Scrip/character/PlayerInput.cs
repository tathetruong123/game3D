using Fusion;
using UnityEngine;

public class PlayerInput : NetworkBehaviour
{
    [Networked] public float horizontalInput { get; private set; }
    [Networked] public float verticalInput { get; private set; }
    [Networked] public bool attackInput { get; private set; }
    [Networked] public bool jumpInput { get; private set; }
    [Networked] public bool laughInput { get; private set; }
    [Networked] public bool sprintInput { get; private set; }

    public override void FixedUpdateNetwork()
    {
        if (!Object.HasStateAuthority) return; // Chỉ người chơi mới cập nhật input

        // Lấy đầu vào di chuyển
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        sprintInput = Input.GetKey(KeyCode.LeftShift);

        attackInput = Input.GetKey(KeyCode.K);
        jumpInput = Input.GetKey(KeyCode.J);
        laughInput = Input.GetKey(KeyCode.X);

        Debug.Log($"Input: Horiz={horizontalInput}, Vert={verticalInput}, Sprint={sprintInput}");
    }

    public void ResetAttackInput() => attackInput = false;
    public void ResetJumpInput() => jumpInput = false;
    public void ResetLaughInput() => laughInput = false;
}
