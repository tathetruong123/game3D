using System.Collections;
using UnityEngine;
using Fusion;

public class Character : NetworkBehaviour
{
    public CharacterController characterController;
    public float speed = 2f;
    public Vector3 movementVelocity;
    public PlayerInput playerInput;
    public Animator animator;
    public DamageZone damageZone;
    public HP HP;
    public float gravity = 10f;
    public float jumpHeight = 1;
    public GameObject sword;

    public enum CharacterState
    {
        Normal,
        Attack,
        Jump,
        Laugh,
        Hurt,
        Die
    }

    [Networked] public CharacterState curState { get; set; }

    public override void FixedUpdateNetwork()
    {
        if (!Object.HasStateAuthority) return; // Đảm bảo chỉ nhân vật do người chơi điều khiển mới thực hiện xử lý mạng

        if (HP.currentHP <= 0)
        {
            RpcChangeState(CharacterState.Die);
            return;
        }

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        switch (curState)
        {
            case CharacterState.Normal:
                if (!stateInfo.IsName("Jump") && !stateInfo.IsName("Laugh") &&
                    !stateInfo.IsName("Hurt") && !stateInfo.IsName("Attack"))
                {
                    CalculateMovement();
                }
                break;

            case CharacterState.Attack:
            case CharacterState.Jump:
            case CharacterState.Hurt:
            case CharacterState.Laugh:
                movementVelocity = Vector3.zero;
                animator.SetFloat("Speed", 0);
                break;
        }

        // Xử lý trọng lực
        if (characterController.isGrounded)
        {
            movementVelocity.y = -gravity * Runner.DeltaTime;
        }
        else
        {
            movementVelocity.y += gravity * Runner.DeltaTime;
        }

        characterController.Move(movementVelocity * Runner.DeltaTime);
    }

    void CalculateMovement()
    {
        //if (!Object.HasStateAuthority) return;

        Debug.Log($"PlayerInput của {gameObject.name}: Horiz={playerInput.horizontalInput}, Vert={playerInput.verticalInput}");

        // Không reset input ở đây để tránh mất dữ liệu khi cập nhật frame
        float currentSpeed = playerInput.sprintInput ? speed * 2 : speed;
        movementVelocity = new Vector3(playerInput.horizontalInput, 0, playerInput.verticalInput).normalized * currentSpeed;
        animator.SetFloat("Speed", movementVelocity.magnitude);

        if (movementVelocity != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(movementVelocity);
        }
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void RpcChangeState(CharacterState newState, RpcInfo info = default)
    {
        curState = newState;
        switch (newState)
        {
            case CharacterState.Jump:
                animator.SetTrigger("Jump");
                break;
            case CharacterState.Attack:
                animator.SetTrigger("Attack");
                break;
            case CharacterState.Laugh:
                animator.SetTrigger("Laugh");
                break;
            case CharacterState.Hurt:
                animator.SetTrigger("Hurt");
                break;
            case CharacterState.Die:
                sword.transform.SetParent(null);
                sword.GetComponent<Rigidbody>().isKinematic = false;
                animator.SetTrigger("Die");
                break;
        }
    }
}
