using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterController characterController;
    public float speed = 2f;
    public Vector3 movementVelocity;
    public PlayerInput playerInput;
    public Animator animator;

    public DamageZone damageZone;
    public HP HP;

    public float gravity = 10f; // Trọng lực
    public float jumpHeight = 1; // Chiều cao nhảy tối đa

    public GameObject sword; // Kiếm

    // State machine
    public enum CharacterState
    {
        Normal,
        Attack,
        //Jump,
        Laugh,
        Hurt,
        Crouch, // Thêm trạng thái ngồi
        Die
    }

    public CharacterState curState; // Trạng thái hiện tại

    void FixedUpdate()
    {
        if (HP.currentHP <= 0)
        {
            ChangeState(CharacterState.Die);
            return;
        }

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        switch (curState)
        {
            case CharacterState.Normal:
                if (//!stateInfo.IsName("Jump") &&
                    !stateInfo.IsName("Laugh") &&
                    !stateInfo.IsName("Hurt") &&
                    !stateInfo.IsName("Attack") &&
                    !stateInfo.IsName("Crouch"))
                {
                    CalculateMovement();
                }
                break;

            case CharacterState.Attack:
                break;
        }

        if (characterController.isGrounded)
        {
            movementVelocity.y = 0;
        }
        else
        {
            movementVelocity.y += gravity * Time.deltaTime;
        }

        characterController.Move(movementVelocity);
    }

    void CalculateMovement()
    {
        //if (playerInput.jumpInput)
        //{
        //    ChangeState(CharacterState.Jump);
        //    playerInput.jumpInput = false; // Reset input
        //    return;
        //}
        if (playerInput.attackInput)
        {
            ChangeState(CharacterState.Attack);
            playerInput.attackInput = false; // Reset input
            return;
        }

        if (playerInput.laughInput)
        {
            ChangeState(CharacterState.Laugh);
            playerInput.laughInput = false; // Reset input
            return;
        }

        if (playerInput.crouchInput) // Kiểm tra nhấn phím X để ngồi
        {
            ChangeState(CharacterState.Crouch);
            playerInput.crouchInput = false; // Reset input
            return;
        }

        movementVelocity.Set(playerInput.horizontalInput, 0, playerInput.verticalInput);
        movementVelocity.Normalize();
        movementVelocity = Quaternion.Euler(0, -45, 0) * movementVelocity;
        movementVelocity *= speed * Time.deltaTime;

        animator.SetFloat("Speed", movementVelocity.magnitude);

        if (movementVelocity != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(movementVelocity);
        }
    }

    // Chuyển đổi trạng thái
    private void ChangeState(CharacterState newState)
    {
        // Xóa trạng thái cũ
        playerInput.attackInput = false;

        // Thoát trạng thái hiện tại
        switch (curState)
        {
            case CharacterState.Normal:
                break;
            case CharacterState.Attack:
                break;
            case CharacterState.Crouch:
                break;
        }

        // Vào trạng thái mới
        switch (newState)
        {
            case CharacterState.Normal:
                break;

            //case CharacterState.Jump:
            //    animator.SetTrigger("Jump");
            //    StartCoroutine(WaitForAnimation("Jump", () =>
            //    {
            //        ChangeState(CharacterState.Normal);
            //    }));
            //    break;
            case CharacterState.Attack:
                animator.SetTrigger("Attack");
                StartCoroutine(WaitForAnimation("Attack", () =>
                {
                    ChangeState(CharacterState.Normal);
                }));
                break;

            case CharacterState.Laugh:
                animator.SetTrigger("Laugh");
                StartCoroutine(WaitForAnimation("Laugh", () =>
                {
                    ChangeState(CharacterState.Normal);
                }));
                break;

            case CharacterState.Hurt:
                animator.SetTrigger("Hurt");
                StartCoroutine(WaitForAnimation("Hurt", () =>
                {
                    ChangeState(CharacterState.Normal);
                }));
                break;

            case CharacterState.Crouch:
                animator.SetTrigger("Crouch"); // Thiết lập trigger "Crouch"
                break;

            case CharacterState.Die:
                sword.transform.SetParent(null);
                sword.GetComponent<Rigidbody>().isKinematic = false;
                animator.SetTrigger("Die");
                break;
        }
      

        // Cập nhật trạng thái hiện tại
        curState = newState;
    }

    private IEnumerator WaitForAnimation(string animationName, System.Action onComplete)
    {
        // Đợi cho đến khi hoạt ảnh bắt đầu
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName(animationName))
        {
            yield return null;
        }

        // Đợi cho đến khi hoạt ảnh kết thúc
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }

        // Gọi callback sau khi hoàn thành
        onComplete?.Invoke();
    }

    public void OnAttack01End()
    {
        ChangeState(CharacterState.Normal);
    }

    public void EndAttack()
    {
        // damageZone.Attack();
    }
}
